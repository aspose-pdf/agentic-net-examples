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

        // Initialize the PdfFileInfo facade with the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the Title metadata (empty string if not set)
            string title = pdfInfo.Title ?? string.Empty;

            // Append the title information to the log file
            using (StreamWriter writer = new StreamWriter(logPath, append: true))
            {
                writer.WriteLine($"{DateTime.UtcNow:u} Title: {title}");
            }

            Console.WriteLine($"Title metadata written to '{logPath}'.");
        }
    }
}