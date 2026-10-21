using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_title.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (full load is required to modify viewer preferences)
        Document pdfDoc = new Document(inputPath);

        // Attempt to set the viewer preference to display the document title in the window caption.
        // The ViewerPreferences property exists only in newer Aspose.Pdf versions (>= 20.9).
        // Use dynamic access with a runtime guard so the code also compiles with older versions.
        dynamic doc = pdfDoc;
        try
        {
            doc.ViewerPreferences.DisplayDocTitle = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Viewer preference applied (if supported). Output saved to '{outputPath}'.");
    }
}
