using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
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
                // Initialize PdfFileInfo facade for the PDF document
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    // Read the Title metadata (fallback to empty string if not set)
                    string title = info.Title ?? string.Empty;

                    // Write the title to the log file (overwrites any existing content)
                    File.WriteAllText(logPath, $"Title: {title}");
                }

                Console.WriteLine($"Title metadata written to '{logPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
