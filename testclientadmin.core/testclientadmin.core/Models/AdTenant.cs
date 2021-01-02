using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace testclientadmin.core.Models {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_tenant", DisableSyncStructure = true)]
	public partial class AdTenant {

		[JsonProperty, Column(IsPrimary = true)]
		public int Id { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string Code { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string Name { get; set; }

		[JsonProperty]
		public int DbType { get; set; }

		[JsonProperty, Column(StringLength = 500)]
		public string ConnectionString { get; set; }

		[JsonProperty]
		public int? IdleTime { get; set; }

		[JsonProperty]
		public bool Enabled { get; set; }

		[JsonProperty, Column(StringLength = 500)]
		public string Description { get; set; }

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
