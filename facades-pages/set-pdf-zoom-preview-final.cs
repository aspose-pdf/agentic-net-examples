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

        // ---------- Preview: zoom = 0.5 ----------
        using (PdfPageEditor previewEditor = new PdfPageEditor())
        {
            // Load the source PDF
            previewEditor.BindPdf(inputPath);
            // Set zoom coefficient for preview (50%)
            previewEditor.Zoom = 0.5f;
            // Apply the changes to the document
            previewEditor.ApplyChanges();
            // Save the preview PDF
            previewEditor.Save(previewPath);
        }

        // ---------- Final output: zoom = 1.0 ----------
        using (PdfPageEditor finalEditor = new PdfPageEditor())
        {
            // Load the source PDF again
            finalEditor.BindPdf(inputPath);
            // Set zoom coefficient for final output (100%)
            finalEditor.Zoom = 1.0f;
            // Apply the changes to the document
            finalEditor.ApplyChanges();
            // Save the final PDF
            finalEditor.Save(finalPath);
        }

        Console.WriteLine($"Preview PDF saved to '{previewPath}'.");
        Console.WriteLine($"Final PDF saved to '{finalPath}'.");
    }
}