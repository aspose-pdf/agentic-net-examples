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

        // Initialize PdfFileInfo facade with the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Read the Title metadata
            string title = pdfInfo.Title;

            // Prepare log entry
            string logEntry = $"Title: {(string.IsNullOrEmpty(title) ? "(none)" : title)}";

            // Write the log entry to the specified log file
            File.WriteAllText(logPath, logEntry);

            Console.WriteLine($"Title metadata written to '{logPath}'.");
        }
    }
}