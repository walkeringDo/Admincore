using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace testclientadmin.core.Models {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_opration_log", DisableSyncStructure = true)]
	public partial class AdOprationLog {

		[JsonProperty, Column(IsPrimary = true)]
		public int Id { get; set; }

		[JsonProperty]
		public int? CreatedUserId { get; set; }

		[JsonProperty, Column(StringLength = -2)]
		public string Params { get; set; }

		[JsonProperty, Column(StringLength = -2)]
		public string Result { get; set; }

		[JsonProperty, Column(StringLength = 500)]
		public string Msg { get; set; }

		[JsonProperty]
		public bool Status { get; set; }

		[JsonProperty]
		public int ElapsedMilliseconds { get; set; }

		[JsonProperty, Column(StringLength = -2)]
		public string BrowserInfo { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string Device { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string Os { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string Browser { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string IP { get; set; }

		[JsonProperty, Column(StringLength = 60)]
		public string NickName { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string ApiMethod { get; set; }

		[JsonProperty, Column(StringLength = 500)]
		public string ApiPath { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string ApiLabel { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string CreatedUserName { get; set; }

		[JsonProperty]
		public DateTime? CreatedTime { get; set; }

	}

}
