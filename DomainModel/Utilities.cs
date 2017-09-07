using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public static class Utilities
    {
        public enum TransactionType
        {
            Credit,
            Debit
        }

        public enum RecurrenceInterval
        {
            None=0,
            Daily,
            Weekly,
            Monthly,
            Yearly
        }

        public static IDictionary<int, string> GetEnumDict<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<int, string>();

            foreach (int value in Enum.GetValues(enumerationType))
            {
                var name = Enum.GetName(enumerationType, value);
                dictionary.Add(value, name);
            }

            return dictionary;
        }
    }

}
