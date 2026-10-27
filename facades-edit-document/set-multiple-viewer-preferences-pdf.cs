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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Attempt to set viewer preferences using dynamic dispatch.
                // This works with newer Aspose.Pdf versions that expose ViewerPreferences,
                // and gracefully degrades for older versions where the property is absent.
                dynamic d = doc;
                try
                {
                    d.ViewerPreferences.CenterWindow = true;   // Open the window centered on the screen
                    d.ViewerPreferences.HideToolbar = true;   // Hide the toolbar when the PDF is opened
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf library version.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with combined viewer preferences to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}