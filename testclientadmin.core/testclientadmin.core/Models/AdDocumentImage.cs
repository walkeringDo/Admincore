using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace testclientadmin.core.Models {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_document_image", DisableSyncStructure = true)]
	public partial class AdDocumentImage {

		[JsonProperty, Column(IsPrimary = true)]
		public int Id { get; set; }

		[JsonProperty]
		public int DocumentId { get; set; }

		[JsonProperty]
		public string Url { get; set; }

		[JsonProperty]
		public int? CreatedUserId { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string CreatedUserName { get; set; }

		[JsonProperty]
		public DateTime? CreatedTime { get; set; }

	}

}
