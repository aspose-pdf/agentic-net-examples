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
            // PdfFileInfo implements IDisposable, so wrap it in a using block
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Read the Title metadata; fallback to empty string if null
                string title = info.Title ?? string.Empty;

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