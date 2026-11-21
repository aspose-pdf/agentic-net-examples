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

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // ViewerPreferences was added in newer Aspose.Pdf versions.
        // To keep the code compatible with older versions we access it via dynamic.
        dynamic doc = pdfDoc;
        try
        {
            var prefs = doc.ViewerPreferences;
            // If the property exists, enable DisplayDocTitle so the PDF title appears in the window caption.
            prefs.DisplayDocTitle = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping DisplayDocTitle setting.");
        }

        // Save the updated PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Processing completed. Saved to '{outputPath}'.");
    }
}
