using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "ToolbarScrollHidden.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdf = new Document(inputPath);

            // Use dynamic access to ViewerPreferences so the code compiles against older
            // Aspose.Pdf versions that do not expose this property. If the property is
            // missing at runtime a RuntimeBinderException is caught and a friendly message
            // is written; otherwise the preferences are applied.
            dynamic doc = pdf;
            try
            {
                // Hide the toolbar and the window UI (scrollbars, etc.) in the viewer.
                doc.ViewerPreferences.HideToolbar = true;
                doc.ViewerPreferences.HideWindowUI = true;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf version.");
            }

            // Save the modified PDF.
            pdf.Save(outputPath);
            Console.WriteLine($"PDF saved with hidden toolbar and scrollbars: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}