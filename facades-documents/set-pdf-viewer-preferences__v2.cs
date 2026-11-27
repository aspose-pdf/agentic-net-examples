using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "viewCustom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Attempt to set viewer preferences. The ViewerPreferences property exists only in newer
                // Aspose.Pdf releases. By using dynamic we bypass compile‑time checks and handle the case
                // where the property is missing at runtime.
                dynamic doc = pdfDoc;
                try
                {
                    doc.ViewerPreferences.CenterWindow = true;
                    doc.ViewerPreferences.HideMenubar = true;
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf version. Skipping this step.");
                }

                // Save the (potentially) modified PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Viewer preferences applied (if supported) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}