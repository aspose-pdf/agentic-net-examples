using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "fullscreen_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF. Use dynamic to access ViewerPreferences so the code compiles
        // with older Aspose.Pdf versions that do not expose the ViewerPreferences type.
        dynamic doc = new Document(inputPath);

        try
        {
            // Attempt to set the FullScreen viewer preference.
            // The PageMode enum is part of Aspose.Pdf and is available even in older releases.
            doc.ViewerPreferences.PageMode = PageMode.FullScreen;
        }
        catch (RuntimeBinderException)
        {
            // The current Aspose.Pdf version does not support ViewerPreferences.
            Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf version. " +
                              "Consider upgrading the Aspose.Pdf NuGet package to enable FullScreen mode.");
        }
        catch (Exception ex)
        {
            // Any other unexpected error while setting the preference.
            Console.WriteLine($"An error occurred while setting viewer preferences: {ex.Message}");
        }

        // Save the (potentially) modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with FullScreen preference (if supported): {outputPath}");
    }
}
