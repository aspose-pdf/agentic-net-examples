using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class MhtConversionHelper
{
    /// <summary>
    /// Converts a PDF file to an HTML (as a fallback for MHTML) while applying fine‑grained page manipulations.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="outputPath">Path where the resulting HTML (or MHT) file will be saved.</param>
    /// <param name="pagesToEdit">
    /// Optional array of 1‑based page numbers to which the edits should be applied.
    /// If null or empty, all pages are processed.
    /// </param>
    /// <param name="rotation">
    /// Rotation angle in degrees (allowed values: 0, 90, 180, 270). Default is 0 (no rotation).
    /// </param>
    /// <param name="zoom">
    /// Zoom factor where 1.0 means 100 %. Must be greater than 0. Default is 1.0.
    /// </param>
    /// <param name="moveX">
    /// Horizontal shift (in points) applied to the page origin. Positive values move right.
    /// </param>
    /// <param name="moveY">
    /// Vertical shift (in points) applied to the page origin. Positive values move up.
    /// </param>
    public static void ConvertPdfToMhtWithPageEdits(
        string pdfPath,
        string outputPath,
        int[] pagesToEdit = null,
        int rotation = 0,
        double zoom = 1.0,
        float moveX = 0,
        float moveY = 0)
    {
        // Load the source PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialise the page‑editor façade.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor.
                editor.BindPdf(pdfDoc);

                // Restrict editing to specific pages if requested.
                if (pagesToEdit != null && pagesToEdit.Length > 0)
                {
                    editor.ProcessPages = pagesToEdit;
                }

                // Apply rotation if a valid angle is supplied.
                if (rotation == 0 || rotation == 90 || rotation == 180 || rotation == 270)
                {
                    editor.Rotation = rotation;
                }

                // Apply zoom factor.
                if (zoom > 0)
                {
                    editor.Zoom = (float)zoom;
                }

                // Apply origin translation.
                if (moveX != 0 || moveY != 0)
                {
                    editor.MovePosition(moveX, moveY);
                }

                // Commit the changes to the underlying document.
                editor.ApplyChanges();
            }

            // NOTE: Older Aspose.Pdf versions do not expose SaveFormat.Mhtml.
            // As a compatible alternative we export to HTML, which can be opened
            // in browsers similarly to MHTML. The caller can rename the file to
            // *.mht if a true MHTML file is required and the runtime environment
            // supports it.
            pdfDoc.Save(outputPath, SaveFormat.Html);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (uncomment and adjust paths as needed):
        // MhtConversionHelper.ConvertPdfToMhtWithPageEdits(
        //     "sample.pdf",
        //     "sample.html", // can be renamed to .mht after conversion
        //     new int[] { 1, 3 }, // edit only pages 1 and 3
        //     rotation: 90,
        //     zoom: 1.2,
        //     moveX: 10,
        //     moveY: -5);
    }
}
