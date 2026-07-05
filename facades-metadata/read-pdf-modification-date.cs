using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before attempting to open it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: The file '{pdfPath}' was not found.");
            return;
        }

        // Initialize PdfFileInfo facade for the PDF file.
        // Using the constructor that accepts the file path ensures the object is properly initialized.
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Retrieve the ModDate string (e.g., "D:20230702120000+00'00'")
            string rawModDate = info.ModDate;

            // Remove the leading "D:" if present.
            if (rawModDate.StartsWith("D:", StringComparison.OrdinalIgnoreCase))
                rawModDate = rawModDate.Substring(2);

            // PDF date format can include timezone like "+05'30'". Replace the apostrophes for parsing.
            string cleaned = rawModDate.Replace("'", string.Empty);

            // Define possible date patterns (with and without timezone).
            string[] patterns =
            {
                "yyyyMMddHHmmsszzz",   // with timezone offset (e.g., +05:30)
                "yyyyMMddHHmmsszz",    // with timezone offset without colon
                "yyyyMMddHHmmss",      // without timezone
                "yyyyMMdd"             // date only
            };

            if (DateTime.TryParseExact(
                    cleaned,
                    patterns,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal,
                    out DateTime modDate))
            {
                // Output formatted date.
                Console.WriteLine("Modification Date: " + modDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                // Fallback to the raw string if parsing fails.
                Console.WriteLine("Modification Date (raw): " + info.ModDate);
            }
        }
    }
}
