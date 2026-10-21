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

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Use dynamic access to ViewerPreferences to stay compatible with older Aspose.Pdf versions
        dynamic doc = pdfDocument;
        try
        {
            var prefs = doc.ViewerPreferences;
            bool currentHideMenubar = prefs.HideMenubar;
            Console.WriteLine($"Current HideMenubar: {currentHideMenubar}");

            // Modify the setting to hide the menu bar
            prefs.HideMenubar = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping modification.");
        }
        catch (Exception ex)
        {
            // Unexpected errors should be reported but not crash the program
            Console.WriteLine($"An unexpected error occurred while accessing ViewerPreferences: {ex.Message}");
        }

        // Save the updated PDF (if the property existed the change is persisted)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF with updated viewer preferences saved to '{outputPath}'.");
    }
}
