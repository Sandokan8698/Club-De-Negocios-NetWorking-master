
using System.Collections.Generic;
using System.Text;
using Domain_Layer.BusinessRules;

namespace Domain_Layer
{
    // abstract business object class
    // ** Enterprise Design Pattern: Domain Model

    public abstract class BusinessObject
    {
        // list of business rules

        List<BusinessRule> rules = new List<BusinessRule>();

        // list of validation errors (following validation failure)

        List<string> errors = new List<string>();

        
        // gets list of validations errors
        
        public List<string> Errors
        {
            get { return errors; }
        }

        
        // adds a business rule to the business object
        
        protected void AddRule(BusinessRule rule)
        {
            rules.Add(rule);
        }

        // determines whether business rules are valid or not.
        // creates a list of validation errors when appropriate
        
        public bool IsValid()
        {
            bool valid = true;

            errors.Clear();

            foreach (var rule in rules)
            {
                if (!rule.Validate(this))
                {
                    valid = false;
                    errors.Add(rule.Error);
                }
            }
            return valid;
        }

        protected string GetPropertyValidationsError(string propertyName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var businessRule in rules)
            {
                if (businessRule.Property == propertyName)
                {
                    if (!businessRule.Validate(this))
                        sb.AppendLine(businessRule.Error);
                }
            }

            return sb.ToString();
        }
    }
}
