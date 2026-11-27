using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string summaryPath = "summary.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Open the PDF with PdfContentEditor (high‑level facade).
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Retrieve the DisplayDocTitle viewer preference.
            // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // To keep the code compatible with older versions we access it via dynamic
            // and handle the possible RuntimeBinderException gracefully.
            bool displayDocTitle = false;
            try
            {
                dynamic doc = editor.Document; // dynamic bypasses compile‑time checks
                var prefs = doc.ViewerPreferences; // may throw at runtime if missing
                displayDocTitle = prefs.DisplayDocTitle;
            }
            catch (RuntimeBinderException)
            {
                // ViewerPreferences (or DisplayDocTitle) is not available in the current library version.
                displayDocTitle = false;
            }
            catch (Exception ex)
            {
                // Any other unexpected error – log and continue with default value.
                Console.Error.WriteLine($"Error accessing ViewerPreferences: {ex.Message}");
                displayDocTitle = false;
            }

            // Write the status to a plain‑text summary file.
            string summary = $"DisplayDocTitle: {displayDocTitle}";
            File.WriteAllText(summaryPath, summary);
        }

        Console.WriteLine($"Summary written to '{summaryPath}'.");
    }
}
