using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Exchange.Core.Helpers
{
    /// <summary>
    /// Enum Service
    /// </summary>
    public static class EnumService
    {
        /// <summary>
        /// Enum Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<SelectOption> GetOptions<T>()
        {
            var _ = typeof(T);
            var result = Enum.GetNames(_)
                .Select(it => new SelectOption
                {
                    Value = (int)Enum.Parse(_, it),
                    Description = GetDescription<T>(it)
                }).ToList();
            return result;
        }

        /// <summary>
        /// Get Description
        /// </summary>
        /// <param name="enumValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDescription<T>(string enumValue)
        {
            var descriptionAttribute = typeof(T)
                .GetField(enumValue)
                ?.GetCustomAttributes(
                    typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null
                ? descriptionAttribute.Description : enumValue;
        }
    }
}