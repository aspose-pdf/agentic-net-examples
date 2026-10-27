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

        // Load the source PDF document
        Document pdfDocument = new Document(inputPath);

        // Attempt to set HideToolbar viewer preference if the property exists in the used Aspose.Pdf version
        try
        {
            dynamic doc = pdfDocument; // bypass compile‑time member check
            doc.ViewerPreferences.HideToolbar = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }

        // Save the PDF with the (potentially) updated viewer preference
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved with HideToolbar set to true (if supported): {outputPath}");
    }
}
