using System;
using System.Globalization;

namespace Zip.InstallmentsService.Api.Common.Extensions;

public static class Iso8601StringExtensions
{
    public static bool TryParseIso8601Date(this string iso8601Date, out DateTime result)
    {
        if (string.IsNullOrWhiteSpace(iso8601Date))
        {
            result = new DateTime();
            return false;
        }

        if (!DateTime.TryParseExact(iso8601Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var result1))
        {
            result = new DateTime();
            return false;
        }

        result = result1;
        return true;
    }

    public static bool TryParseIso8601DateTimeToUtc(
        this string iso8601DateTime,
        out DateTime result)
    {
        if (string.IsNullOrWhiteSpace(iso8601DateTime))
        {
            result = new DateTime();
            return false;
        }

        if (!DateTime.TryParse(iso8601DateTime, null, DateTimeStyles.RoundtripKind, out var result1))
        {
            result = new DateTime();
            return false;
        }

        result = result1.ToUniversalTime();
        return true;
    }
}