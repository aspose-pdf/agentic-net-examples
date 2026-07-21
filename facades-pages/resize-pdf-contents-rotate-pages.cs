using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string resizedPath = "resized.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Resize contents to 80% of original size (percentage based)
        double newWidthPercent = 80;
        double newHeightPercent = 80;

        // Resize using PdfFileEditor (percentage based resizing)
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool resizeSuccess = fileEditor.ResizeContentsPct(
            inputPath,          // source PDF
            resizedPath,        // destination PDF after resizing
            null,               // null = all pages
            newWidthPercent,    // new width in percent
            newHeightPercent);  // new height in percent

        if (!resizeSuccess)
        {
            Console.Error.WriteLine("Failed to resize PDF contents.");
            return;
        }

        // Rotate all pages by 90 degrees using PdfPageEditor
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(resizedPath);
            pageEditor.Rotation = 90; // rotate 90 degrees
            pageEditor.ApplyChanges();
            pageEditor.Save(outputPath);
        }

        // Optional: delete intermediate file
        try { File.Delete(resizedPath); } catch { }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}