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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // The ViewerPreferences property exists only in newer versions of Aspose.Pdf.
                // To keep the code compatible with older versions we access it dynamically and
                // handle the case where the property is absent at runtime.
                dynamic d = doc;
                try
                {
                    d.ViewerPreferences.HideToolbar = true;
                    d.ViewerPreferences.HideMenubar = true;
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with hidden toolbar and menu bar (if supported): {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
