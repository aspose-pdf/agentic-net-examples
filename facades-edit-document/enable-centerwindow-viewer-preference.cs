using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Required for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "centered_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the current Aspose.Pdf API
        Document pdfDocument = new Document(inputPath);

        // Attempt to set the CenterWindow viewer preference.
        // The ViewerPreferences property exists only in newer versions of Aspose.Pdf.
        // To keep the source compatible with older versions we access it via dynamic
        // and handle the possible RuntimeBinderException gracefully.
        dynamic docDynamic = pdfDocument;
        try
        {
            // If the property exists, set CenterWindow to true.
            docDynamic.ViewerPreferences.CenterWindow = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf library version. Skipping CenterWindow setting.");
        }

        // Save the PDF with (or without) the updated viewer preference
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
