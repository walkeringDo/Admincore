using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace testclientadmin.core.Models {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_permission", DisableSyncStructure = true)]
	public partial class AdPermission {

		[JsonProperty, Column(IsPrimary = true)]
		public int Id { get; set; }

		[JsonProperty]
		public int? ModifiedUserId { get; set; }

		[JsonProperty]
		public DateTime? CreatedTime { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string CreatedUserName { get; set; }

		[JsonProperty]
		public int? CreatedUserId { get; set; }

		[JsonProperty]
		public bool IsDeleted { get; set; }

		[JsonProperty]
		public int Version { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string Description { get; set; }

		[JsonProperty]
		public int? Sort { get; set; }

		[JsonProperty]
		public bool? External { get; set; }

		[JsonProperty]
		public bool? NewWindow { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string ModifiedUserName { get; set; }

		[JsonProperty]
		public bool? Opened { get; set; }

		[JsonProperty]
		public bool Enabled { get; set; }

		[JsonProperty]
		public bool Hidden { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string Icon { get; set; }

		[JsonProperty, Column(StringLength = 500)]
		public string Path { get; set; }

		[JsonProperty]
		public int? ApiId { get; set; }

		[JsonProperty]
		public int? ViewId { get; set; }

		[JsonProperty]
		public int Type { get; set; }

		[JsonProperty, Column(StringLength = 550)]
		public string Code { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string Label { get; set; }

		[JsonProperty]
		public int ParentId { get; set; }

		[JsonProperty]
		public bool? Closable { get; set; }

		[JsonProperty]
		public DateTime? ModifiedTime { get; set; }

	}

}
