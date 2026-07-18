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
            // Load PDF metadata using PdfFileInfo facade
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
            {
                // Read the Title property (may be null)
                string title = pdfInfo.Title ?? string.Empty;

                // Append the title to the log file
                File.AppendAllText(logPath, $"Title: {title}{Environment.NewLine}");
            }

            Console.WriteLine($"Title metadata written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}