using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Paths for the source and destination PDFs.
        const string sourcePath = "input.pdf";
        const string destinationPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal source PDF if it does not already exist.
        // This makes the example self‑contained and eliminates the FileNotFound
        // exception that occurred when the original code tried to read a missing
        // "input.pdf" file.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(sourcePath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF for resizing test."));
                doc.Save(sourcePath);
            }
        }

        // Initialize the PDF file editor.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // ---------------------------------------------------------------------
        // Create resize parameters with a mix of percentage and absolute margins.
        // Left  = 10% of page width
        // Right = 30 units (points)
        // Top   = 5% of page height
        // Bottom= 20 units (points)
        // Width and Height are set to Auto so they are calculated from the
        // margins.
        // ---------------------------------------------------------------------
        PdfFileEditor.ContentsResizeParameters parameters =
            new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10),   // left margin
                PdfFileEditor.ContentsResizeValue.Auto(),       // content width
                PdfFileEditor.ContentsResizeValue.Units(30),    // right margin
                PdfFileEditor.ContentsResizeValue.Percents(5),  // top margin
                PdfFileEditor.ContentsResizeValue.Auto(),       // content height
                PdfFileEditor.ContentsResizeValue.Units(20)     // bottom margin
            );

        // Resize contents of all pages using the hybrid parameters.
        // Passing null for the pages array processes every page.
        fileEditor.ResizeContents(sourcePath, destinationPath, null, parameters);

        Console.WriteLine($"Resized PDF saved to '{destinationPath}'.");
    }
}
