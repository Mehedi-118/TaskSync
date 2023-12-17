namespace PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser
{
    public class AccesslistVM : BaseModel
	{
		public string? ModuleName { get; set; }
		public string ControllerName { get; set; }
		public string ActionName { get; set; }
		public string Mask { get; set; }
		public byte AccessStatus { get; set; }
		public byte IsVisible { get; set; }
		public string IconClass { get; set; }
		public int BaseModule { get; set; }
		public int? BaseModuleIndex { get; set; }
	}
}
