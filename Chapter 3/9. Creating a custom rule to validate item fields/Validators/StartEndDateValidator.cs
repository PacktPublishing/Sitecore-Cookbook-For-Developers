using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using System;
using System.Runtime.Serialization;

namespace SitecoreCookbook.Validators
{
    [Serializable]
    public class StartEndDateValidator : StandardValidator
    {
        public StartEndDateValidator() { }

        public StartEndDateValidator(SerializationInfo info, StreamingContext context)
            : base(info, context){}

        protected override ValidatorResult Evaluate()
        {
            Item item = base.GetItem();

            string fieldStart = this.Parameters["StartDate"];
            string fieldEnd = this.Parameters["EndDate"];

            DateField startDate = item.Fields[fieldStart];
            DateField endDate = item.Fields[fieldEnd];

            if (startDate.DateTime != DateTime.MinValue && startDate.DateTime > endDate.DateTime)
            {
                this.Text = String.Format(fieldStart + " ({0}) can't be greater than " + fieldEnd + " ({1})", startDate.DateTime, endDate.DateTime);
                return ValidatorResult.CriticalError;
            }

            return ValidatorResult.Valid;
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.CriticalError);
        }

        public override string Name
        {
            get { return "Start Date can not be greater than End Date"; }
        }
    }
}