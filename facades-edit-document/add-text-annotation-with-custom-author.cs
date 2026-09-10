using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";
        const string authorName = "John Doe";

        // -----------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal placeholder if needed.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // -----------------------------------------------------------------
        // Step 1: Add a text annotation (sticky note) to page 1.
        // -----------------------------------------------------------------
        using (var contentEditor = new PdfContentEditor())
        {
            // Load the source PDF.
            contentEditor.BindPdf(inputPath);

            // Define the annotation rectangle (left, bottom, width, height).
            // System.Drawing.Rectangle constructor: (x, y, width, height).
            // Original PDF coordinates: left=100, bottom=500, right=300, top=600.
            // Width = right - left = 200, Height = top - bottom = 100.
            var annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create the text annotation.
            // Parameters: rectangle, title, contents, open flag, icon name, page number.
            contentEditor.CreateText(
                annotRect,
                "Review Note",               // title (appears in the popup header)
                "Please verify this section.", // contents
                true,                        // open when the PDF is opened
                "Note",                     // icon type
                1);                          // page number (1‑based)

            // Save the PDF with the new annotation.
            contentEditor.Save(outputPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Set custom author metadata for the annotation(s).
        // -----------------------------------------------------------------
        using (var annotationEditor = new PdfAnnotationEditor())
        {
            // Load the PDF that now contains the annotation.
            annotationEditor.BindPdf(outputPath);

            // Determine the page range to process (here we affect all pages).
            int startPage = 1;
            int endPage   = annotationEditor.Document.Pages.Count;

            // srcAuthor: the existing author to replace (empty because we just created it).
            string srcAuthor = "";
            string desAuthor = authorName;

            // Modify the author of annotations in the specified page range.
            annotationEditor.ModifyAnnotationsAuthor(startPage, endPage, srcAuthor, desAuthor);

            // Overwrite the same file with the updated author information.
            annotationEditor.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and author set to '{authorName}'. Output saved to '{outputPath}'.");
    }
}
