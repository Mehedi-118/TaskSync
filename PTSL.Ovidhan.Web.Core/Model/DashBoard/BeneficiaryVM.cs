namespace PTSL.Ovidhan.Web.Core.Model.DashBoard;

public class BeneficiaryVM
{
    public long TotalBeneficiary { get; set; }

    public long TotalMale { get; set; }
    public long TotalFemale { get; set; }
    public double TotalMalePercentage { get; set; }
    public double TotalFemalePercentage { get; set; }

    public long TotalGotTraining { get; set; }
    public double TotalGotTrainingPercentage { get; set; }

    public long TotalGotLoan { get; set; }
    public double TotalGotLoanPercentage { get; set; }

    public long TotalLoanRepayment { get; set; }
    public double TotalLoanRepaymentPercentage { get; set; }

    public long TotalEvilWealth { get; set; }
    public double TotalEvilWealthPercentage { get; set; }
}