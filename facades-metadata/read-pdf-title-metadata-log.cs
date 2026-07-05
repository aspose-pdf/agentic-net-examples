using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logPath = "metadata.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileInfo facade for the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Read the Title metadata (may be null)
            string title = pdfInfo.Title ?? string.Empty;

            // Write the title to a simple log file
            try
            {
                File.WriteAllText(logPath, $"Title: {title}");
                Console.WriteLine($"Title written to '{logPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error writing log: {ex.Message}");
            }
        }
    }
}