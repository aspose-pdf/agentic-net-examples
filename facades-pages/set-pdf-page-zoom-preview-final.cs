using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string previewPath = "preview.pdf";
        const string finalPath   = "final.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a preview version with 50% zoom
        using (PdfPageEditor previewEditor = new PdfPageEditor())
        {
            previewEditor.BindPdf(inputPath);   // load the PDF
            previewEditor.Zoom = 0.5f;          // set preview zoom to 0.5 (50%)
            previewEditor.Save(previewPath);   // save the preview PDF
        }

        // Create the final version with 100% zoom
        using (PdfPageEditor finalEditor = new PdfPageEditor())
        {
            finalEditor.BindPdf(inputPath);    // load the PDF again
            finalEditor.Zoom = 1.0f;           // set final zoom to 1.0 (100%)
            finalEditor.Save(finalPath);       // save the final PDF
        }

        Console.WriteLine($"Preview PDF saved to '{previewPath}' (zoom 0.5).");
        Console.WriteLine($"Final PDF saved to '{finalPath}' (zoom 1.0).");
    }
}