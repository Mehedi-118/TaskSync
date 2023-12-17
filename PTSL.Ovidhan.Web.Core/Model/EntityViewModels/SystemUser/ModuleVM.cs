namespace PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser
{
	public class ModuleVM : BaseModel
	{
		public string ModuleName { get; set; }
		public string ModuleIcon { get; set; }
		public byte IsVisible { get; set; }
		public int? MenueOrder { get; set; }
	}
}
