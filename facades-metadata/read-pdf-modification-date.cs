using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfFileInfo implements IDisposable, so wrap it in a using block
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // ModDate is returned as a string by Aspose.Pdf.Facades.PdfFileInfo.
                // Convert the string to a DateTime before formatting.
                string modDateString = info.ModDate;
                if (DateTime.TryParse(modDateString, out DateTime modDate))
                {
                    // Format the date as desired (e.g., "2023-08-15 14:30:00")
                    string formattedDate = modDate.ToString("yyyy-MM-dd HH:mm:ss");
                    Console.WriteLine($"Modification Date: {formattedDate}");
                }
                else
                {
                    Console.Error.WriteLine($"Unable to parse modification date: '{modDateString}'");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur while reading the PDF
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
