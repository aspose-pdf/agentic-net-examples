using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PdfFileInfo facade inside a using block for deterministic disposal
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Retrieve the raw ModDate string from the PDF metadata
            string rawModDate = info.ModDate;

            // Try to parse the PDF date format and output a friendly representation
            if (TryParsePdfDate(rawModDate, out DateTime modDate))
            {
                // Format the date as "yyyy-MM-dd HH:mm:ss"
                Console.WriteLine($"Modification date: {modDate:yyyy-MM-dd HH:mm:ss}");
            }
            else
            {
                // Fallback: output the raw string if parsing fails
                Console.WriteLine($"Modification date (raw): {rawModDate}");
            }
        }
    }

    // Parses PDF date strings such as "D:20230818120000+00'00'"
    static bool TryParsePdfDate(string pdfDate, out DateTime result)
    {
        result = default;
        if (string.IsNullOrEmpty(pdfDate))
            return false;

        // Remove the leading "D:" if present
        string s = pdfDate.StartsWith("D:") ? pdfDate.Substring(2) : pdfDate;

        // Separate the date/time part from the timezone offset
        string datePart = s;
        string tzPart = "";
        int tzPos = s.IndexOf('+');
        if (tzPos < 0) tzPos = s.IndexOf('-');
        if (tzPos > 0)
        {
            datePart = s.Substring(0, tzPos);
            tzPart = s.Substring(tzPos);
        }

        // Ensure the date part has at least "yyyyMMddHHmmss"
        datePart = datePart.PadRight(14, '0');
        const string format = "yyyyMMddHHmmss";

        if (!DateTime.TryParseExact(datePart, format, null,
            System.Globalization.DateTimeStyles.None, out result))
            return false;

        // Apply timezone offset if it exists (e.g., +05'30' or -02'00')
        if (!string.IsNullOrEmpty(tzPart))
        {
            // Remove any single quotes
            tzPart = tzPart.Replace("'", "");

            // Extract sign, hour, and minute components
            string sign = tzPart.Substring(0, 1);
            if (tzPart.Length >= 5 &&
                int.TryParse(tzPart.Substring(1, 2), out int hour) &&
                int.TryParse(tzPart.Substring(3, 2), out int minute))
            {
                TimeSpan offset = new TimeSpan(hour, minute, 0);
                // Adjust the parsed time according to the sign
                result = sign == "-" ? result.Add(offset) : result.Subtract(offset);
            }
        }

        return true;
    }
}