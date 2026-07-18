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

        // Initialize PdfFileInfo with the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the ModDate string from the PDF metadata
            string modDateStr = pdfInfo.ModDate;

            if (string.IsNullOrWhiteSpace(modDateStr))
            {
                Console.WriteLine("ModDate is not set in the PDF.");
                return;
            }

            // PDF dates are typically in the format "D:yyyyMMddHHmmssK"
            // Attempt to parse using known patterns; fallback to generic parsing
            DateTime modDate;
            string[] patterns = {
                "D:yyyyMMddHHmmsszzz",
                "D:yyyyMMddHHmmss",
                "yyyyMMddHHmmss",
                "yyyy-MM-ddTHH:mm:ss"
            };

            bool parsed = DateTime.TryParseExact(
                modDateStr,
                patterns,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out modDate);

            if (!parsed && !DateTime.TryParse(modDateStr, out modDate))
            {
                Console.WriteLine($"Unable to parse ModDate: {modDateStr}");
                return;
            }

            // Output the modification date in a readable format
            Console.WriteLine($"Modification date: {modDate:yyyy-MM-dd HH:mm:ss}");
        }
    }
}