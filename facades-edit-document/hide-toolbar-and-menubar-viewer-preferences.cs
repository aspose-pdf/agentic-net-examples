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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Use dynamic access to ViewerPreferences to stay compatible with older Aspose.Pdf versions.
                // If the property does not exist, a RuntimeBinderException will be thrown and we fall back gracefully.
                dynamic dDoc = doc;
                try
                {
                    dDoc.ViewerPreferences.HideToolbar = true;
                    dDoc.ViewerPreferences.HideMenubar = true;
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping UI preferences.");
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Viewer preferences applied (if supported) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
