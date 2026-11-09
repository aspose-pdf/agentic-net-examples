using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

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
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Attempt to enable HideScrollbars viewer preference.
            // The ViewerPreferences property exists only in newer Aspose.Pdf versions (e.g., 20.9+).
            // Using dynamic bypasses compile‑time checks and allows the code to compile against older versions.
            dynamic docDyn = pdfDoc;
            try
            {
                docDyn.ViewerPreferences.HideScrollbars = true;
                Console.WriteLine("HideScrollbars viewer preference applied.");
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
