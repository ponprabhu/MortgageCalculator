using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MortgageCalculator.Dto;

namespace MortgageCalculator.Api.Repos
{
    public interface IMortgageRepo
    {
        List<Mortgage> GetAllMortgages();
    }

    public class MortgageRepo : IMortgageRepo
    {
        public List<Mortgage> GetAllMortgages()
        {
            using (var context = new MortgageData.MortgageDataContext())
            {
                var mortgages = context.Mortgages.ToList();
                List<Mortgage> result = new List<Mortgage>();
                foreach (var mortgage in mortgages)
                {
                    result.Add(new Mortgage()
                    {
                        Name = mortgage.Name,
                        EffectiveStartDate = mortgage.EffectiveStartDate,
                        EffectiveEndDate = mortgage.EffectiveEndDate,
                        CancellationFee = mortgage.CancellationFee,
                        EstablishmentFee = mortgage.CancellationFee,
                        InterestRepayment = (InterestRepayment)Enum.Parse(typeof(InterestRepayment), mortgage.InterestRepayment.ToString()),
                        MortgageId = mortgage.MortgageId,
                        MortgageType = (MortgageType)Enum.Parse(typeof(MortgageType), mortgage.MortgageType.ToString()),
                        MortgageTypeText = mortgage.MortgageType.ToString(),
                        InterestRate = mortgage.InterestRate,
                        TermsInMonths = MonthDifference(mortgage.EffectiveEndDate, mortgage.EffectiveStartDate)
                    }
                    );
                }
                return result;
            }
        }

        private int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }
    }
}