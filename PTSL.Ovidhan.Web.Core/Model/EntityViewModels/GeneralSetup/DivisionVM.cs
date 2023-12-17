using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Web.Core.Model.GeneralSetup
{
    public class DivisionVM : BaseModel
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string NameBn { get; set; }
        public List<DistrictVM>? DistrictList { get; set; }
        //public IList<PermanentAddress> PermanentAddresses { get; set; }
        //public IList<PresentAddress> PresentAddresses { get; set; }
        //public IList<SpouseInformation> SpouseInformation { get; set; }
        //public IList<EmployeeInformation> EmployeeInformation { get; set; }
    }
}
