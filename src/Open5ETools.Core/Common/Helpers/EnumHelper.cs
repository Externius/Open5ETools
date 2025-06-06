﻿using System.ComponentModel;

namespace Open5ETools.Core.Common.Helpers;

public static class EnumHelper
{
    public static string GetDescription<T>(this T enumerationValue) where T : struct
    {
        var type = enumerationValue.GetType();
        if (!type.IsEnum)
            throw new ArgumentException(@"EnumerationValue must be of Enum type", nameof(enumerationValue));

        var memberInfo = type.GetMember(enumerationValue.ToString()!);
        if (memberInfo.Length <= 0)
            return enumerationValue.ToString() ?? string.Empty;
        var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : enumerationValue.ToString() ?? string.Empty;
    }
}