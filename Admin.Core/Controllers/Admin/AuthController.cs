using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Admin.Core.Attributes;
using Admin.Core.Common.Auth;
using Admin.Core.Common.Output;
using Admin.Core.Service.Admin.Auth;
using Admin.Core.Service.Admin.Auth.Input;
using Admin.Core.Service.Admin.Auth.Output;
using Admin.Core.Service.Admin.LoginLog;
using Admin.Core.Service.Admin.LoginLog.Input;
using Admin.Core.Common.Helpers;
using Admin.Core.Service.Admin.User;
using Admin.Core.Common.Extensions;
using System.Net.Http;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 授权管理
    /// </summary>
    public class AuthController : AreaController
    {
        private readonly IUserToken _userToken;
        private readonly IAuthService _authService;
        private readonly IUserService _userServices;
        private readonly ILoginLogService _loginLogService;

        public AuthController(
            IUserToken userToken,
            IAuthService authServices,
            IUserService userServices,
            ILoginLogService loginLogService
        )
        {
            _userToken = userToken;
            _authService = authServices;
            _userServices = userServices;
            _loginLogService = loginLogService;
        }

        /// <summary>
        /// 获得token
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private IResponseOutput GetToken(ResponseOutput<AuthLoginOutput> output)
        {
            if (!output.Success)
            {
                return ResponseOutput.NotOk(output.Msg);
            }

            var user = output.Data;
            var token = _userToken.Create(new[]
            {
                new Claim(ClaimAttributes.UserId, user.Id.ToString()),
                new Claim(ClaimAttributes.UserName, user.UserName),
                new Claim(ClaimAttributes.UserNickName, user.NickName),
                new Claim(ClaimAttributes.TenantId, user.TenantId.ToString())
            });

            return ResponseOutput.Ok(new { token });
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="lastKey">上次验证码键</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [NoOprationLog]
        public async Task<IResponseOutput> GetVerifyCode(string lastKey)
        {
            return await _authService.GetVerifyCodeAsync(lastKey);
        }

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [NoOprationLog]
        public async Task<IResponseOutput> GetPassWordEncryptKey()
        {
            return await _authService.GetPassWordEncryptKeyAsync();
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Login]
        public async Task<IResponseOutput> GetUserInfo()
        {
            return await _authService.GetUserInfoAsync();
        }

        protected HttpClient Client { get; }
        /// <summary>
        /// 用户登录
        /// 根据登录信息生成Token
        /// </summary>
        /// <param name="input">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [NoOprationLog]
        public async Task<IResponseOutput> Login(AuthLoginInput input)
        {
           

            var sw = new Stopwatch();
            sw.Start();
            var res = await _authService.LoginAsync(input);
            sw.Stop();

            #region 添加登录日志
            var loginLogAddInput = new LoginLogAddInput()
            {
                CreatedUserName = input.UserName,
                ElapsedMilliseconds = sw.ElapsedMilliseconds,
                Status = res.Success,
                Msg = res.Msg
            };

            ResponseOutput<AuthLoginOutput> output = null;
            if (res.Success)
            {
                output = (res as ResponseOutput<AuthLoginOutput>);
                var user = output.Data;
                loginLogAddInput.CreatedUserId = user.Id;
                loginLogAddInput.NickName = user.NickName;
            }

            await _loginLogService.AddAsync(loginLogAddInput);
            #endregion

            if (!res.Success)
            {
                return res;
            }

            IResponseOutput outm = GetToken(output);
            return GetToken(output);
        }



        //public async Task Login(AuthLoginInput input = null)
        //{
        //    if (input == null && _appConfig.VarifyCode.Enable)
        //    {
        //        var res = await _authService.GetVerifyCodeAsync("") as IResponseOutput<AuthGetVerifyCodeOutput>;
        //        var verifyCodeKey = string.Format(CacheKey.VerifyCodeKey, res.Data.Key);
        //        var verifyCode = await _cache.GetAsync(verifyCodeKey);
        //        input = new AuthLoginInput()
        //        {
        //            UserName = "admin",
        //            Password = "111111",
        //            VerifyCodeKey = res.Data.Key,
        //            VerifyCode = verifyCode
        //        };
        //    }

        //    //Client.DefaultRequestHeaders.Connection.Add("keep-alive");
        //    Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");

        //    var result = await Client.PostAsync($"/api/admin/auth/login", GetHttpContent(input));
        //    var content = await result.Content.ReadAsStringAsync();
        //    var jObject = JsonConvert.DeserializeObject<JObject>(content);

        //    Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jObject["data"]["token"]}");
        //}

        /// <summary>
        /// 刷新Token
        /// 以旧换新
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IResponseOutput> Refresh([BindRequired] string token)
        {
            var userClaims = _userToken.Decode(token);
            if(userClaims == null || userClaims.Length == 0)
            {
                return ResponseOutput.NotOk();
            }

            var refreshExpires = userClaims.FirstOrDefault(a => a.Type == ClaimAttributes.RefreshExpires)?.Value;
            if (refreshExpires.IsNull())
            {
                return ResponseOutput.NotOk();
            }

            if(refreshExpires.ToLong() <= DateTime.Now.ToTimestamp())
            {
                return ResponseOutput.NotOk("登录信息已过期");
            }

            var userId = userClaims.FirstOrDefault(a => a.Type == ClaimAttributes.UserId)?.Value;
            if (userId.IsNull())
            {
                return ResponseOutput.NotOk();
            }
            var output = await _userServices.GetLoginUserAsync(userId.ToLong());

            return GetToken(output);
        }
    }
}
