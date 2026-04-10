using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string logPath = "metadata.log";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Initialize PdfFileInfo with the PDF file path
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
            {
                // Read the Title metadata (may be null)
                string title = pdfInfo.Title ?? string.Empty;

                // Write the title to a log file
                File.WriteAllText(logPath, $"Title: {title}");

                Console.WriteLine($"Title extracted and written to '{logPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}