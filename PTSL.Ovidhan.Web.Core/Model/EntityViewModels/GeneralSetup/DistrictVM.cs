using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Model.GeneralSetup
{
    public class DistrictVM : BaseModel
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string NameBn { get; set; }
        public long DivisionId { get; set; }
        public DivisionVM? Division { get; set; }
        //public IList<PermanentAddressVM> PermanentAddresses { get; set; }
        //public IList<PresentAddressVM> PresentAddresses { get; set; }
        //public IList<SpouseInformationVM> SpouseInformation { get; set; }
        //public IList<EmployeeInformationVM> EmployeeInformation { get; set; }

        //public string DistrictName { get; set; }
        //public long DivisionId { get; set; }
        //public virtual Division Division { get; set; }
        //public IList<PoliceStation> PoliceStations { get; set; }
        //public IList<PermanentAddress> PermanentAddresses { get; set; }
        //public IList<PresentAddress> PresentAddresses { get; set; }
        //public IList<SpouseInformation> SpouseInformation { get; set; }
        //public IList<EmployeeInformation> EmployeeInformation { get; set; }
    }
}
