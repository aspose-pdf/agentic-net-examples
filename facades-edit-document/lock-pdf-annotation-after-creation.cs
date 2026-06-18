using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "locked_annotation.pdf";
        const string tempPdf   = "temp_with_annotation.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Create an annotation using the Facades API (PdfContentEditor)
        // ------------------------------------------------------------
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            // Bind the source PDF
            contentEditor.BindPdf(inputPdf);

            // Define the annotation rectangle (x, y, width, height)
            // NOTE: PdfContentEditor.CreateRubberStamp expects System.Drawing.Rectangle
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a rubber‑stamp annotation on page 1
            // Parameters: pageNumber, rectangle, icon, contents, color
            contentEditor.CreateRubberStamp(
                page: 1,
                annotRect: annotRect,
                icon: "Draft",
                annotContents: "Sample locked annotation",
                color: System.Drawing.Color.Red);

            // Save the PDF that now contains the annotation
            contentEditor.Save(tempPdf);
        }

        // ------------------------------------------------------------
        // 2. Lock the newly created annotation using the core API
        // ------------------------------------------------------------
        using (Document doc = new Document(tempPdf))
        {
            // Annotations are stored per page; page indexing is 1‑based
            Page page = doc.Pages[1];

            // Find the annotation we just added (there is only one on this page)
            Annotation annotation = null;
            foreach (Annotation ann in page.Annotations)
            {
                // Identify by its Contents text (or any other property you prefer)
                if (ann.Contents == "Sample locked annotation")
                {
                    annotation = ann;
                    break;
                }
            }

            if (annotation != null)
            {
                // Set the Locked flag – prevents moving, resizing or deleting by the user
                annotation.Flags = AnnotationFlags.Locked;
            }
            else
            {
                Console.Error.WriteLine("Failed to locate the created annotation.");
            }

            // Save the final PDF with the locked annotation
            doc.Save(outputPdf);
        }

        // Clean up the intermediate file
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF saved with locked annotation: {outputPdf}");
    }
}
