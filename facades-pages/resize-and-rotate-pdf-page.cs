using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string resizedPath = "resized.pdf";
        const string outputPath  = "rotated.pdf";

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a simple one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            // Create a one‑page PDF with a placeholder text.
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF – generated automatically"));
                doc.Save(inputPath);
            }
        }

        // ------------------------------------------------------------
        // Step 1: Resize page contents to 80% of original size.
        // ------------------------------------------------------------
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool resizeOk = fileEditor.ResizeContentsPct(
            inputPath,          // source PDF
            resizedPath,        // destination PDF after resizing
            null,               // null = all pages
            80,                 // new width = 80% of original
            80);                // new height = 80% of original

        if (!resizeOk)
        {
            Console.Error.WriteLine("Content resize failed.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Rotate the resized page(s) by 90 degrees.
        // ------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(resizedPath);
            pageEditor.Rotation = 90;   // valid values: 0, 90, 180, 270
            pageEditor.ApplyChanges();
            // Save the rotated result to a new file.
            // ApplyChanges writes changes back to the bound file, so we copy it.
            File.Copy(resizedPath, outputPath, true);
        }

        Console.WriteLine($"Page resized and rotated successfully: {outputPath}");
    }
}
