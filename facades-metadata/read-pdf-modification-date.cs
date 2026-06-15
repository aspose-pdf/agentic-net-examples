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

        // Initialize the PdfFileInfo facade with the PDF file
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Retrieve the ModDate string (PDF format, e.g., "D:20230815123045+02'00'")
        string modDateRaw = pdfInfo.ModDate;

        if (string.IsNullOrEmpty(modDateRaw))
        {
            Console.WriteLine("ModDate is not set in the document.");
            return;
        }

        // Remove the leading "D:" if present
        if (modDateRaw.StartsWith("D:", StringComparison.Ordinal))
            modDateRaw = modDateRaw.Substring(2);

        // Convert PDF timezone format (+HH'mm' or -HH'mm') to a .NET compatible format (+HH:mm)
        if (modDateRaw.Length > 0 && (modDateRaw[0] == '+' || modDateRaw[0] == '-'))
        {
            int apostropheIdx = modDateRaw.IndexOf('\'');
            if (apostropheIdx > 0 && apostropheIdx + 2 < modDateRaw.Length)
            {
                string hourPart = modDateRaw.Substring(0, apostropheIdx);
                string minutePart = modDateRaw.Substring(apostropheIdx + 1, 2);
                modDateRaw = $"{hourPart}:{minutePart}";
            }
        }

        // Define possible date formats that may appear in the PDF metadata
        string[] possibleFormats = new[]
        {
            "yyyyMMddHHmmsszzz", // with timezone offset like +02:00
            "yyyyMMddHHmmss",    // without timezone
            "yyyyMMddHHmmssK",   // generic with timezone
            "yyyyMMddHHmmss'Z'"  // UTC indicated by 'Z'
        };

        // Try to parse the date using the known formats
        if (!DateTime.TryParseExact(modDateRaw,
                                    possibleFormats,
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out DateTime modDate))
        {
            // Fallback to a more permissive parse if exact formats fail
            if (!DateTime.TryParse(modDateRaw, out modDate))
            {
                Console.WriteLine($"Unable to parse ModDate: {pdfInfo.ModDate}");
                return;
            }
        }

        // Output the modification date in a readable format
        Console.WriteLine($"Modification date: {modDate:yyyy-MM-dd HH:mm:ss}");
    }
}