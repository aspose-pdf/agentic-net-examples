using System;
using System.Globalization;
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

        // Initialize PdfFileInfo facade for the PDF file
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Verify that the input is a valid PDF
            if (!info.IsPdfFile)
            {
                Console.WriteLine("The specified file is not a valid PDF.");
                return;
            }

            // Retrieve the ModDate string (e.g., "D:20230415123045+02'00'")
            string modDateStr = info.ModDate;
            if (string.IsNullOrEmpty(modDateStr))
            {
                Console.WriteLine("ModDate property is not set in the PDF.");
                return;
            }

            // Remove the leading "D:" if present
            string cleaned = modDateStr.StartsWith("D:") ? modDateStr.Substring(2) : modDateStr;

            // Convert PDF timezone format (+HH'mm' or -HH'mm') to .NET (+HH:mm)
            int tzPos = cleaned.LastIndexOf('+');
            if (tzPos == -1) tzPos = cleaned.LastIndexOf('-');
            if (tzPos > 0 && tzPos + 5 <= cleaned.Length)
            {
                string sign = cleaned[tzPos].ToString();
                string hour = cleaned.Substring(tzPos + 1, 2);
                string minute = cleaned.Substring(tzPos + 4, 2);
                cleaned = cleaned.Substring(0, tzPos) + sign + hour + ":" + minute;
            }

            // Define possible date formats
            string[] formats = {
                "yyyyMMddHHmmsszzz", // with timezone offset
                "yyyyMMddHHmmss",    // without timezone
                "yyyyMMddHHmmss'Z'"  // UTC Z suffix
            };

            // Try to parse the cleaned string
            if (DateTime.TryParseExact(cleaned, formats, CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal, out DateTime modDate))
            {
                // Output the date in a readable format
                Console.WriteLine($"Modification date: {modDate:yyyy-MM-dd HH:mm:ss}");
            }
            else
            {
                // Fallback: output the raw string if parsing fails
                Console.WriteLine($"Modification date (raw): {modDateStr}");
            }
        }
    }
}