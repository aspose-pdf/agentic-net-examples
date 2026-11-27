using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException when using dynamic

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "output.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so we can use a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPdfPath);

            // Retrieve the CenterWindow viewer preference.
            // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // To keep the code compatible with older versions we access it via dynamic and
            // handle the possible RuntimeBinderException gracefully.
            bool centerWindow = false;
            try
            {
                dynamic doc = editor.Document; // dynamic bypasses compile‑time checks
                var prefs = doc.ViewerPreferences; // may throw at runtime if property missing
                centerWindow = prefs.CenterWindow;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf library version. Assuming CenterWindow = false.");
                // centerWindow remains false (default) or you could set a sentinel value.
            }
            catch (Exception ex)
            {
                // Unexpected errors while reading the preference.
                Console.WriteLine($"Error retrieving CenterWindow setting: {ex.Message}");
            }

            // Write the result to a CSV file.
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
            {
                // Header row.
                csvWriter.WriteLine("FileName,CenterWindow");
                // Data row.
                csvWriter.WriteLine($"{Path.GetFileName(inputPdfPath)},{centerWindow}");
            }

            Console.WriteLine($"CenterWindow={centerWindow} written to {outputCsvPath}");
        }
    }
}
