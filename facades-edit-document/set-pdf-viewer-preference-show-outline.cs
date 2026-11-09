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

        // Attempt to set the PageMode to UseOutlines. The ViewerPreferences property
        // exists only in newer versions of Aspose.Pdf. Use dynamic access to keep the
        // code compatible with older versions and guard with a RuntimeBinderException.
        dynamic doc = pdfDocument;
        try
        {
            // This will succeed when the library version supports ViewerPreferences.
            doc.ViewerPreferences.PageMode = PageMode.UseOutlines;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping viewer preference configuration.");
        }

        // Save the updated PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Viewer preference applied (if supported) and saved to '{outputPath}'.");
    }
}
