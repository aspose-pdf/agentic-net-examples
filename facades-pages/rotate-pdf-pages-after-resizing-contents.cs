using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string tempPath   = "temp_resized.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 1. Determine original page size (assumes all pages have the same size)
        double originalWidth, originalHeight;
        using (Document doc = new Document(inputPath))
        {
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            originalWidth  = firstPage.PageInfo.Width;
            originalHeight = firstPage.PageInfo.Height;
        }

        // 2. Resize contents: swap width and height to keep layout after rotation
        PdfFileEditor fileEditor = new PdfFileEditor();
        // null pages array means all pages are processed
        fileEditor.ResizeContents(
            inputPath,          // source PDF
            tempPath,           // destination PDF after resizing
            null,               // all pages
            originalHeight,     // new width (previous height)
            originalWidth);     // new height (previous width)

        // 3. Rotate pages by 90 degrees
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(tempPath);
        pageEditor.Rotation = 90; // rotate all pages
        pageEditor.ApplyChanges();
        pageEditor.Save(outputPath);
        pageEditor.Close();

        // 4. Clean up temporary file
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}