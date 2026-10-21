using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

// Alias to disambiguate System.IO.Path from Aspose.Pdf.Drawing.Path (if the drawing namespace is added later).
using IOPath = System.IO.Path;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "output.pdf";
        const string tempPagePath   = "page4_temp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract page 4 to a temporary file using Aspose.Pdf.Facades.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        // Extract page 4 (startPage = endPage = 4) to a separate PDF.
        editor.Extract(inputPath, 4, 4, tempPagePath);

        // -----------------------------------------------------------------
        // 2. Load the extracted page, locate the existing stamp annotation,
        //    and reposition it 10 mm (≈28.3465 points) from the left margin.
        // -----------------------------------------------------------------
        using (Document pageDoc = new Document(tempPagePath))
        {
            // The extracted file contains only one page (1‑based index).
            Page page = pageDoc.Pages[1];

            // Find the first StampAnnotation on the page.
            StampAnnotation stamp = null;
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is StampAnnotation s)
                {
                    stamp = s;
                    break;
                }
            }

            if (stamp != null)
            {
                // Current rectangle of the stamp.
                Aspose.Pdf.Rectangle oldRect = stamp.Rect;

                // Width and height of the stamp.
                double width  = oldRect.URX - oldRect.LLX;
                double height = oldRect.URY - oldRect.LLY;

                // 10 mm in points (1 mm = 2.834645669 points).
                const double mmToPt = 2.834645669;
                double leftMargin = 10 * mmToPt; // ≈28.3465 points

                // New rectangle aligned to the left margin + 10 mm.
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    leftMargin,                 // LLX
                    oldRect.LLY,                // LLY (keep vertical position)
                    leftMargin + width,         // URX
                    oldRect.LLY + height        // URY
                );

                // Apply the new rectangle.
                stamp.Rect = newRect;
            }
            else
            {
                Console.WriteLine("No stamp annotation found on page 4.");
            }

            // Save the modified page back to the temporary file.
            pageDoc.Save(tempPagePath);
        }

        // -----------------------------------------------------------------
        // 3. Load the original document, remove the original page 4, and insert
        //    the modified page at the same position.
        // -----------------------------------------------------------------
        Document sourceDoc = new Document(inputPath);
        // Remove the original page 4.
        sourceDoc.Pages.Delete(4);

        // Load the modified page (it contains a single page).
        Document modifiedPageDoc = new Document(tempPagePath);
        // Insert the modified page at position 4.
        sourceDoc.Pages.Insert(4, modifiedPageDoc.Pages[1]);

        // Save the final document.
        sourceDoc.Save(outputPath);

        // -----------------------------------------------------------------
        // 4. Clean up temporary files.
        // -----------------------------------------------------------------
        try { File.Delete(tempPagePath); } catch { }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}
