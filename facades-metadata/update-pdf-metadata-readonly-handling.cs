using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileInfo facade with the source PDF
        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            // Example of updating a metadata field
            info.Title = "Updated Title";

            // Attempt to save the updated PDF; handle read‑only output file scenario
            bool saved = false;
            try
            {
                saved = info.SaveNewInfo(outputPdf);
            }
            catch (IOException ioEx)
            {
                // If the failure is due to a read‑only attribute, clear it and retry
                if (File.Exists(outputPdf) && (File.GetAttributes(outputPdf) & FileAttributes.ReadOnly) != 0)
                {
                    File.SetAttributes(outputPdf, FileAttributes.Normal);
                    // Retry saving after attribute change
                    saved = info.SaveNewInfo(outputPdf);
                }
                else
                {
                    // Re‑throw if it's a different I/O issue
                    throw;
                }
            }

            if (saved)
                Console.WriteLine($"Metadata saved successfully to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Failed to save updated PDF. Check LastException for details.");
        }
    }
}