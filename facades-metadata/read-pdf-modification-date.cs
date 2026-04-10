using System;
using System.IO;
using System.Globalization;
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

        try
        {
            // Initialize PdfFileInfo for the given PDF file
            using (PdfFileInfo info = new PdfFileInfo(inputPath))
            {
                // ModDate is stored as a string in PDF date format (e.g., "D:20230815123045+02'00'")
                string modDateStr = info.ModDate;

                if (string.IsNullOrEmpty(modDateStr))
                {
                    Console.WriteLine("ModDate not set in the PDF.");
                    return;
                }

                // Remove the leading "D:" if present
                if (modDateStr.StartsWith("D:"))
                    modDateStr = modDateStr.Substring(2);

                // Convert PDF timezone format (+HH'mm' or -HH'mm') to a .NET‑compatible format (+HH:mm)
                if (modDateStr.Length > 0 && (modDateStr[0] == '+' || modDateStr[0] == '-'))
                {
                    int quoteIndex = modDateStr.IndexOf('\'');
                    if (quoteIndex > 0 && quoteIndex + 2 < modDateStr.Length)
                    {
                        string hourPart = modDateStr.Substring(1, quoteIndex - 1);
                        string minutePart = modDateStr.Substring(quoteIndex + 1).Trim('\'');
                        modDateStr = $"{modDateStr[0]}{hourPart}:{minutePart}";
                    }
                }

                // Define possible date formats (without and with timezone)
                string[] formats = {
                    "yyyyMMddHHmmsszzz", // with timezone (+HH:mm)
                    "yyyyMMddHHmmss",    // without timezone
                    "yyyyMMddHHmmss'Z'"  // UTC indicated by 'Z'
                };

                // Try to parse using the defined formats
                if (DateTime.TryParseExact(modDateStr, formats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime modDate))
                {
                    Console.WriteLine($"ModDate: {modDate:yyyy-MM-dd HH:mm:ss}");
                }
                else if (DateTime.TryParse(modDateStr, out modDate))
                {
                    // Fallback to generic parsing if exact formats fail
                    Console.WriteLine($"ModDate: {modDate:yyyy-MM-dd HH:mm:ss}");
                }
                else
                {
                    Console.WriteLine($"Unable to parse ModDate value: {info.ModDate}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}