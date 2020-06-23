using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CE3.Models
{
	public class ChangeStudentLastNameRequest
	{
		public String FirstName { get; set; }
		public String OriginalLastName { get; set; }
		public String NewLastName { get; set; }
	}
}