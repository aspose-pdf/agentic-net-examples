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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access ViewerPreferences via dynamic to stay compatible with older Aspose.Pdf versions
            dynamic dynDoc = doc;
            try
            {
                var prefs = dynDoc.ViewerPreferences;
                Console.WriteLine($"HideMenubar before change: {prefs.HideMenubar}");
                // Modify the HideMenubar flag
                prefs.HideMenubar = true;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPath}'.");
    }
}