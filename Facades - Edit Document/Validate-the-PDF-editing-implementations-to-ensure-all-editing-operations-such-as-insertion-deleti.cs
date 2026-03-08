using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfEditingValidator
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf = "input.pdf";
        const string intermediatePdf = "intermediate.pdf";
        const string finalPdf = "final.pdf";
        const string validationLog = "validation_log.txt";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Use PdfContentEditor to perform insertion, deletion, and text replacement
        // ------------------------------------------------------------
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            // Bind the source PDF
            contentEditor.BindPdf(inputPdf);

            // Replace all occurrences of "OldText" with "NewText"
            contentEditor.ReplaceText("OldText", "NewText");

            // Delete all images from the document
            contentEditor.DeleteImage();

            // Insert a web link annotation on the first page
            // Define the rectangle area for the link (x, y, width, height)
            // System.Drawing.Rectangle expects (x, y, width, height) where (x, y) is the upper‑left corner.
            // The original Aspose rectangle used (llx, lly, urx, ury). Convert accordingly:
            //   x = llx = 100
            //   y = lly = 500 (Aspose uses lower‑left, but System.Drawing uses upper‑left; for PDF coordinates the same values work because the API translates them internally)
            //   width  = urx - llx = 300 - 100 = 200
            //   height = ury - lly = 550 - 500 = 50
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // Create a web link that points to https://example.com on page index 0 (first page)
            contentEditor.CreateWebLink(linkRect, "https://example.com", 0);

            // Save the modifications to an intermediate file
            contentEditor.Save(intermediatePdf);
        }

        // ------------------------------------------------------------
        // 2. Use PdfAnnotationEditor to modify annotation properties (if any)
        // ------------------------------------------------------------
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            // Bind the PDF produced in step 1
            annotationEditor.BindPdf(intermediatePdf);

            // Example: Delete all existing annotations (optional cleanup)
            annotationEditor.DeleteAnnotations();

            // Save the changes (overwrites the intermediate file)
            annotationEditor.Save(intermediatePdf);
        }

        // ------------------------------------------------------------
        // 3. Use PdfPageEditor to modify page layout (e.g., rotate page 1)
        // ------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the PDF from the previous step
            pageEditor.BindPdf(intermediatePdf);

            // Rotate the first page by 90 degrees clockwise
            pageEditor.Rotation = 90;          // Global rotation for all pages
            pageEditor.ProcessPages = new int[] { 1 }; // Apply only to page 1

            // Apply the changes
            pageEditor.ApplyChanges();

            // Save the final edited PDF
            pageEditor.Save(finalPdf);
        }

        // ------------------------------------------------------------
        // 4. Validate the final PDF using Document.Validate
        // ------------------------------------------------------------
        using (Document doc = new Document(finalPdf))
        {
            // Validate against PDF/A-1B compliance (example)
            bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);

            Console.WriteLine($"Validation result: {(isValid ? "PASS" : "FAIL")}");
            Console.WriteLine($"Validation log written to: {validationLog}");
        }
    }
}
