using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class PdfEditingValidator
{
    static void Main()
    {
        // Input PDF, image to insert, and output paths
        const string inputPdfPath = "input.pdf";
        const string imagePath = "insert.png";
        const string outputPdfPath = "edited_output.pdf";
        const string validationLogPath = "validation_log.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // 1. Text replacement and image deletion (PdfContentEditor)
            // -------------------------------------------------
            using (PdfContentEditor contentEditor = new PdfContentEditor())
            {
                // Bind the already loaded document
                contentEditor.BindPdf(doc);

                // Replace all occurrences of "OldText" with "NewText"
                contentEditor.ReplaceText("OldText", "NewText");

                // Delete every image from the document
                contentEditor.DeleteImage();
            }

            // -------------------------------------------------
            // 2. Insert a new image onto page 1 (PdfFileMend)
            // -------------------------------------------------
            using (PdfFileMend fileMend = new PdfFileMend())
            {
                fileMend.BindPdf(doc);

                // Add the image to page 1 at the specified rectangle (llx, lly, urx, ury)
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // Example coordinates: lower‑left (100,500), upper‑right (300,700)
                    fileMend.AddImage(imgStream, 1, 100f, 500f, 300f, 700f);
                }
            }

            // -------------------------------------------------
            // 3. Rotate page 2 by 90 degrees (PdfPageEditor)
            // -------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(doc);

                // Set rotation to 90 degrees
                pageEditor.Rotation = 90;

                // Apply the rotation only to page 2 (1‑based indexing)
                pageEditor.ProcessPages = new int[] { 2 };

                // Commit the changes
                pageEditor.ApplyChanges();
            }

            // -------------------------------------------------
            // 4. Delete all existing annotations and add a new web link (PdfAnnotationEditor + manual LinkAnnotation)
            // -------------------------------------------------
            using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
            {
                annotationEditor.BindPdf(doc);

                // Remove every annotation in the document
                annotationEditor.DeleteAnnotations();
            }

            // Add a new link annotation manually (the API does not expose CreateLinkAnnotation on PdfAnnotationEditor)
            Page firstPage = doc.Pages[1];
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
            LinkAnnotation link = new LinkAnnotation(firstPage, linkRect);
            link.Action = new GoToURIAction("https://example.com");
            firstPage.Annotations.Add(link);

            // -------------------------------------------------
            // 5. Validate the edited PDF (Document.Validate)
            // -------------------------------------------------
            bool isValid = doc.Validate(validationLogPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"PDF/A‑1B validation result: {isValid}");
            if (!isValid)
            {
                Console.WriteLine($"See validation details in: {validationLogPath}");
            }

            // -------------------------------------------------
            // 6. Save the final edited PDF
            // -------------------------------------------------
            doc.Save(outputPdfPath);
            Console.WriteLine($"Edited PDF saved to '{outputPdfPath}'.");
        }
    }
}
