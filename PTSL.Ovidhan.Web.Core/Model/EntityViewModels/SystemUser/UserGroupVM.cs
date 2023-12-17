namespace PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser
{
	public class UserGroupVM : BaseModel
	{
		public string GroupName { get; set; }
		public string GroupDescription { get; set; }
		public byte GroupStatus { get; set; }
		public byte IsVisible { get; set; }
	}
}
