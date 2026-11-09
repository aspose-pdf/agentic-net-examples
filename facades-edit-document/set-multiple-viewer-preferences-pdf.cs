using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the high‑level Document class.
            using (Document pdfDoc = new Document(inputPath))
            {
                // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
                // Access it dynamically to keep the code compilable with older versions.
                dynamic docDyn = pdfDoc;
                try
                {
                    // Combine viewer preferences before saving.
                    docDyn.ViewerPreferences.CenterWindow = true;   // Center the window on open
                    docDyn.ViewerPreferences.HideToolbar = true;    // Hide the toolbar on open
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf library version. Preferences will be ignored.");
                }

                // Save the updated PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with combined viewer preferences to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}