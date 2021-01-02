using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace testclientadmin.core.Models {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_user", DisableSyncStructure = true)]
	public partial class AdUser {

		[JsonProperty, Column(IsPrimary = true)]
		public int Id { get; set; }

		[JsonProperty, Column(StringLength = 60)]
		public string UserName { get; set; }

		[JsonProperty, Column(StringLength = 60)]
		public string Password { get; set; }

		[JsonProperty, Column(StringLength = 60)]
		public string NickName { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string Avatar { get; set; }

		[JsonProperty]
		public int Status { get; set; }

		[JsonProperty, Column(StringLength = 500)]
		public string Remark { get; set; }

		[JsonProperty]
		public int Version { get; set; }

		[JsonProperty]
		public bool IsDeleted { get; set; }

		[JsonProperty]
		public int? CreatedUserId { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string CreatedUserName { get; set; }

		[JsonProperty]
		public DateTime? CreatedTime { get; set; }

		[JsonProperty]
		public int? ModifiedUserId { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string ModifiedUserName { get; set; }

		[JsonProperty]
		public DateTime? ModifiedTime { get; set; }

	}

}
