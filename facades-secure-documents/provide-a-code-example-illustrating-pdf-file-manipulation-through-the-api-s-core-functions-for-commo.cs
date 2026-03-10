using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // GoToURIAction, LinkAnnotation reside in this namespace

class PdfFacadeDemo
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Rotate and zoom pages using PdfPageEditor
            // -------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(doc);                     // Bind the document
                pageEditor.Rotation = 90;                    // Rotate all pages 90 degrees
                pageEditor.Zoom = 1.2f;                      // Zoom to 120% (float literal)
                pageEditor.ApplyChanges();                   // Apply the changes
            }

            // -------------------------------------------------
            // 2. Replace text using PdfContentEditor
            // -------------------------------------------------
            using (PdfContentEditor contentEditor = new PdfContentEditor())
            {
                contentEditor.BindPdf(doc);                  // Bind the document
                // Replace the first occurrence of "OldText" with "NewText"
                contentEditor.ReplaceText("OldText", "NewText");
                // Extract all link annotations and display their URIs
                var links = contentEditor.ExtractLink();
                foreach (var link in links)
                {
                    // The collection may contain generic Annotation objects; ensure we have a LinkAnnotation
                    if (link is LinkAnnotation linkAnnot &&
                        linkAnnot.Action is GoToURIAction uriAction &&
                        !string.IsNullOrEmpty(uriAction.URI))
                    {
                        Console.WriteLine($"Link to: {uriAction.URI}");
                    }
                    else
                    {
                        Console.WriteLine("Link without a URI action found.");
                    }
                }
            }

            // -------------------------------------------------
            // 3. Add an image stamp to the first page using PdfFileMend
            // -------------------------------------------------
            if (File.Exists(imagePath))
            {
                using (PdfFileMend fileMend = new PdfFileMend())
                {
                    fileMend.BindPdf(doc);                  // Bind the document
                    // Add the image to page 1 at coordinates (100,500) with width=300, height=600
                    // The AddImage overload expects float values; literals are suffixed with 'f'
                    fileMend.AddImage(imagePath, new int[] { 1 }, 100f, 500f, 300f, 600f);
                }
            }
            else
            {
                Console.WriteLine($"Image not found: {imagePath} (skipping image stamp)");
            }

            // -------------------------------------------------
            // 4. Create a bookmark for page 1 using PdfBookmarkEditor
            // -------------------------------------------------
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(doc);                 // Bind the document
                bookmarkEditor.CreateBookmarkOfPage("Chapter 1", 1);
            }

            // -------------------------------------------------
            // 5. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
    }
}
