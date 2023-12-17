namespace PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser
{
    public class AccessMapperVM : BaseModel
	{
		public string? MapperName { get; set; }
		public string? AccessList { get; set; }
		public byte RoleStatus { get; set; }
		public byte IsVisible { get; set; }
	}

	public class AccessMapperViewModelWithList
	{
		public long MapperID { get; set; }
		public string? MapperName { get; set; }
		public List<AccesslistVM>? AccessList { get; set; }
		public System.DateTime CreateDate { get; set; }
		public System.DateTime CreateTime { get; set; }
	}

	public class AccessMapperViewModelById
	{
		public long MapperID { get; set; }
		public string? MapperName { get; set; }
		public List<AccesslistVM>? AccessList { get; set; }
		public System.DateTime CreateDate { get; set; }
		public System.DateTime CreateTime { get; set; }
	}

	public class ModuleViewModel
	{
		public long ModuleID { get; set; }
		public string? ModuleName { get; set; }
	}
}
