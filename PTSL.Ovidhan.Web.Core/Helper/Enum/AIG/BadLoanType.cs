using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum.AIG;

public enum BadLoanType
{
    [Display(Name = "Regular")]
    Regular,

    [Display(Name = "Under Observation")]
    UnderObservation,

    [Display(Name = "Inferior Loan")]
    InferiorLoan,

    [Display(Name = "Suspicious Loan")]
    SuspiciousLoan,

    [Display(Name = "Bad Loan")]
    EvilLoan
}

