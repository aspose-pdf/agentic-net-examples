using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo provides access to PDF file metadata, including ModDate.
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // ModDate is returned as a string (PDF date format). Convert it to DateTime.
            string modDateString = info.ModDate;
            DateTime modDate;
            // Try to parse using the standard PDF date format "D:yyyyMMddHHmmssK".
            // If parsing fails, fall back to DateTime.TryParse which handles many formats.
            if (!DateTime.TryParseExact(modDateString,
                                         "'D:'yyyyMMddHHmmssK",
                                         System.Globalization.CultureInfo.InvariantCulture,
                                         System.Globalization.DateTimeStyles.None,
                                         out modDate))
            {
                // Fallback to a more permissive parse.
                DateTime.TryParse(modDateString, out modDate);
            }

            // Format the date as desired, e.g., "yyyy-MM-dd HH:mm:ss".
            string formattedDate = modDate.ToString("yyyy-MM-dd HH:mm:ss");

            Console.WriteLine($"Modification Date: {formattedDate}");
        }
    }
}
