using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_minimal.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Attempt to set viewer preferences for a minimalistic UI.
            // The ViewerPreferences property is available only in newer versions of Aspose.Pdf.
            // Use dynamic access to avoid compile‑time errors on older versions.
            dynamic doc = pdfDoc;
            try
            {
                doc.ViewerPreferences.HideToolbar = true;
                doc.ViewerPreferences.HideMenubar = true;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping UI minimization settings.");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
