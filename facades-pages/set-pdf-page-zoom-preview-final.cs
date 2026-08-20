using System;
using System.IO;
using Aspose.Pdf;
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

        // Create a preview PDF with 50 % zoom
        using (PdfPageEditor previewEditor = new PdfPageEditor())
        {
            previewEditor.BindPdf(inputPath);   // load source PDF
            previewEditor.Zoom = 0.5f;          // set preview zoom
            previewEditor.ApplyChanges();       // apply the zoom change
            previewEditor.Save(previewPath);    // save preview PDF
        }

        // Create the final PDF with 100 % zoom (no scaling)
        using (PdfPageEditor finalEditor = new PdfPageEditor())
        {
            finalEditor.BindPdf(inputPath);     // load source PDF again
            finalEditor.Zoom = 1.0f;            // set final output zoom
            finalEditor.ApplyChanges();         // apply the zoom change
            finalEditor.Save(finalPath);        // save final PDF
        }

        Console.WriteLine("Preview and final PDFs have been generated.");
    }
}