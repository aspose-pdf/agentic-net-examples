using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        // Initialize PdfFileInfo with the PDF file path
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Verify that the file is a valid PDF
        if (!pdfInfo.IsPdfFile)
        {
            Console.WriteLine("The specified file is not a valid PDF.");
            return;
        }

        // Retrieve the raw ModDate string
        string rawModDate = pdfInfo.ModDate;
        Console.WriteLine("ModDate (raw): " + rawModDate);

        // Attempt to parse and format the date
        string cleaned = rawModDate;
        if (cleaned.StartsWith("D:"))
        {
            cleaned = cleaned.Substring(2);
        }
        // Remove apostrophes that may appear in the timezone part
        cleaned = cleaned.Replace("'", string.Empty);

        DateTime parsedDate;
        bool success = DateTime.TryParseExact(
            cleaned,
            "yyyyMMddHHmmsszzz",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out parsedDate) ||
            DateTime.TryParseExact(
                cleaned,
                "yyyyMMddHHmmss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDate);

        if (success)
        {
            Console.WriteLine("ModDate (formatted): " + parsedDate.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        else
        {
            Console.WriteLine("Unable to parse ModDate; displayed raw value.");
        }
    }
}
