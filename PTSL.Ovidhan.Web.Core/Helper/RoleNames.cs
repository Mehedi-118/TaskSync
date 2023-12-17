namespace PTSL.Ovidhan.Web.Core.Helper;

public class RoleNames
{
    public const string BeatOfficer = "Beat Officer";
    public const string RangeOfficer = "Range Officer";
    public const string CMO_ACF = "CMO/ACF";
    public const string DFO = "DFO";
    public const string SUPER_ADMIN = "Super Admin";
    public const string Beneficiary = "Beneficiary Group";

    public const string GENERAL_INDEX_PERMISSION = $"{DFO},{SUPER_ADMIN},{BeatOfficer},{RangeOfficer},{CMO_ACF}";
    public const string GENERAL_CREATE_PERMISSION = $"{DFO},{SUPER_ADMIN},{BeatOfficer},{RangeOfficer},{CMO_ACF}";
    public const string GENERAL_EDIT_PERMISSION = $"{DFO},{SUPER_ADMIN},{BeatOfficer},{RangeOfficer},{CMO_ACF}";
    public const string GENERAL_DETAILS_PERMISSION = $"{DFO},{SUPER_ADMIN},{BeatOfficer},{RangeOfficer},{CMO_ACF}";
    public const string GENERAL_DELETE_PERMISSION = $"{DFO},{SUPER_ADMIN},{BeatOfficer},{RangeOfficer},{CMO_ACF}";
    public const string GENERAL_CRUD_PERMISSION = $"{DFO},{SUPER_ADMIN},{BeatOfficer},{RangeOfficer},{CMO_ACF}";

    public const string BENEFICIARY_CREATE_PERMISSION = $"{DFO},{SUPER_ADMIN}";
    public const string BENEFICIARY_EDIT_PERMISSION = $"{DFO},{SUPER_ADMIN},{BeatOfficer},{RangeOfficer},{CMO_ACF}";
    public const string BENEFICIARY_DELETE_PERMISSION = $"{DFO},{SUPER_ADMIN}";

    public const string TRANSACTION_PERMISSION = $"{DFO},{SUPER_ADMIN},{CMO_ACF}";
}

