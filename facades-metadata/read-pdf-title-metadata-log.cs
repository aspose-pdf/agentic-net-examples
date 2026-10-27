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
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfFileInfo implements IDisposable – wrap in using for deterministic cleanup
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Read the Title metadata property
                string title = info.Title ?? string.Empty;

                // Append the title to the log file
                string logEntry = $"Title: {title}{Environment.NewLine}";
                File.AppendAllText(logPath, logEntry);
            }

            Console.WriteLine($"Title metadata written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}