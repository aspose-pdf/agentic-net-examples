using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for SignatureField

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all signature fields from the document's form (core API, no Facades)
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>();

            if (signatureFields == null || !signatureFields.Any())
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            int i = 1;
            foreach (var sigField in signatureFields)
            {
                var signature = sigField.Signature;
                if (signature == null)
                {
                    Console.WriteLine($"Signature {i}: No signature data available.");
                    i++;
                    continue;
                }

                // The signing time is stored in the signature dictionary under the "M" key (PDF date format).
                DateTime signingTimeLocal = ExtractSigningTimeFromSignature(signature);
                DateTime signingTimeUtc = signingTimeLocal.Kind == DateTimeKind.Utc ? signingTimeLocal : signingTimeLocal.ToUniversalTime();

                Console.WriteLine($"Signature {i}:");
                Console.WriteLine($"  Signing time (local) : {signingTimeLocal:O}");
                Console.WriteLine($"  Signing time (UTC)   : {signingTimeUtc:O}");
                i++;
            }
        }
    }

    /// <summary>
    /// Extracts the signing time from a Signature object's underlying PDF dictionary using reflection.
    /// The PDF date is stored in the "M" entry (e.g., D:20231113104530+02'00').
    /// </summary>
    private static DateTime ExtractSigningTimeFromSignature(Signature signature)
    {
        if (signature == null)
            return DateTime.MinValue;

        // Use reflection to obtain the internal "Dictionary" property (it is not part of the public API in recent versions).
        PropertyInfo dictProp = signature.GetType().GetProperty("Dictionary", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (dictProp == null)
            return DateTime.MinValue;

        var dictObj = dictProp.GetValue(signature) as IDictionary<string, object>;
        if (dictObj == null || !dictObj.ContainsKey("M"))
            return DateTime.MinValue;

        string pdfDate = dictObj["M"]?.ToString();
        return ParsePdfDate(pdfDate);
    }

    /// <summary>
    /// Parses a PDF date string (e.g., "D:20231113104530+02'00'") into a DateTime.
    /// The returned DateTime is of Kind Unspecified; timezone offsets are applied to obtain UTC.
    /// </summary>
    private static DateTime ParsePdfDate(string pdfDate)
    {
        if (string.IsNullOrEmpty(pdfDate))
            return DateTime.MinValue;

        // Remove the leading "D:" if present.
        if (pdfDate.StartsWith("D:"))
            pdfDate = pdfDate.Substring(2);

        // Basic components – default to the earliest valid values if missing.
        int year   = pdfDate.Length >= 4 ? int.Parse(pdfDate.Substring(0, 4)) : 1;
        int month  = pdfDate.Length >= 6 ? int.Parse(pdfDate.Substring(4, 2)) : 1;
        int day    = pdfDate.Length >= 8 ? int.Parse(pdfDate.Substring(6, 2)) : 1;
        int hour   = pdfDate.Length >=10 ? int.Parse(pdfDate.Substring(8, 2)) : 0;
        int minute = pdfDate.Length >=12 ? int.Parse(pdfDate.Substring(10, 2)) : 0;
        int second = pdfDate.Length >=14 ? int.Parse(pdfDate.Substring(12, 2)) : 0;

        // Build a DateTime without timezone information.
        var localDt = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Unspecified);

        // Handle optional timezone offset (e.g., +02'00' or -05'30').
        if (pdfDate.Length > 14)
        {
            char tzSign = pdfDate[14];
            if (tzSign == '+' || tzSign == '-')
            {
                // Expect format: +hh'mm' or -hh'mm'
                if (pdfDate.Length >= 19)
                {
                    int tzHour   = int.Parse(pdfDate.Substring(15, 2));
                    int tzMinute = int.Parse(pdfDate.Substring(18, 2));
                    var offset = new TimeSpan(tzHour, tzMinute, 0);
                    if (tzSign == '-')
                        offset = -offset;

                    // Convert to UTC by subtracting the offset.
                    var utcDt = DateTime.SpecifyKind(localDt - offset, DateTimeKind.Utc);
                    return utcDt;
                }
            }
        }

        // No timezone information – treat as unspecified local time.
        return localDt;
    }
}
