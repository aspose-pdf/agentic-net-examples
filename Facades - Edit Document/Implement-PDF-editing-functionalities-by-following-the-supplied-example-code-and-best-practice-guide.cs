using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // needed for Rectangle and other annotation types

class PdfEditingDemo
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "edited_output.pdf";
        const string imagePath = "stamp.png";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF document (Document implements IDisposable)
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 2. Replace all occurrences of a word using PdfContentEditor
            // -----------------------------------------------------------------
            using (PdfContentEditor contentEditor = new PdfContentEditor())
            {
                // Bind the loaded Document instance
                contentEditor.BindPdf(doc);

                // Replace the word "Sample" with "Demo"
                contentEditor.ReplaceText("Sample", "Demo");

                // No explicit Save needed here; changes are applied to the bound Document
            }

            // -----------------------------------------------------------------
            // 3. Add an image to the first page using PdfFileMend
            // -----------------------------------------------------------------
            using (PdfFileMend fileMend = new PdfFileMend())
            {
                fileMend.BindPdf(doc);

                // Add the image to page 1 at coordinates (100, 500) with width=200, height=100
                // Parameters: image file path, page number, lower-left X, lower-left Y, width, height
                fileMend.AddImage(imagePath, 1, 100f, 500f, 200f, 100f);
            }

            // -----------------------------------------------------------------
            // 4. Add a web link annotation to the first page (using concrete LinkAnnotation)
            // -----------------------------------------------------------------
            // Define the rectangle area for the link (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 400, 300, 420);
            // Get the first page
            Page firstPage = doc.Pages[1];
            // Create the link annotation and assign a URI action
            LinkAnnotation link = new LinkAnnotation(firstPage, linkRect)
            {
                Action = new GoToURIAction("https://www.example.com")
            };
            // Add the annotation to the page
            firstPage.Annotations.Add(link);

            // -----------------------------------------------------------------
            // 5. Rotate all pages 90 degrees clockwise using PdfPageEditor
            // -----------------------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(doc);

                // Set rotation for all pages (valid values: 0, 90, 180, 270)
                pageEditor.Rotation = 90;

                // Apply the rotation changes
                pageEditor.ApplyChanges();
            }

            // -----------------------------------------------------------------
            // 6. Save the modified document (PDF output, no SaveOptions needed)
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF editing completed. Output saved to '{outputPdf}'.");
    }
}
