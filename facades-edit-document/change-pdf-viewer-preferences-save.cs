using System;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        Document pdfDoc;
        try
        {
            pdfDoc = new Document(inputPath);
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        // Attempt to set viewer preferences if the current Aspose.Pdf version supports it.
        try
        {
            dynamic docDyn = pdfDoc;
            docDyn.ViewerPreferences.HideToolbar = true;
            docDyn.ViewerPreferences.HideMenubar = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf version. Skipping preference changes.");
        }

        try
        {
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
        }
    }
}
