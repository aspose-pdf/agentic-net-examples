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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Attempt to set the viewer preference to hide the toolbar.
        // The ViewerPreferences property is available only in newer versions of Aspose.Pdf.
        // Use dynamic access to keep the code compatible with older versions.
        dynamic doc = pdfDocument;
        try
        {
            // If the property exists, set HideToolbar to true.
            doc.ViewerPreferences.HideToolbar = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping HideToolbar setting.");
        }

        // Save the PDF with the (potentially) updated viewer preference
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved: {outputPath}");
    }
}
