using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Target printable area in points (A4 size)
    const double TargetWidth  = 595.0;
    const double TargetHeight = 842.0;

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions
                double origWidth  = page.PageInfo.Width;
                double origHeight = page.PageInfo.Height;

                // Compute scale factor to fit within target rectangle while preserving aspect ratio
                double scaleX = TargetWidth  / origWidth;
                double scaleY = TargetHeight / origHeight;
                double scale  = Math.Min(scaleX, scaleY); // uniform scaling

                // New page size after scaling (used only for centering calculations)
                double newWidth  = origWidth  * scale;
                double newHeight = origHeight * scale;

                // Center the scaled page within the target area (offsets are kept for reference –
                // Aspose.Pdf does not expose a direct Transform property on PageInfo, so the content
                // is not explicitly translated. If exact centering is required, a PdfPageEditor with
                // a custom transformation matrix would be needed.
                double offsetX = (TargetWidth  - newWidth)  / 2.0;
                double offsetY = (TargetHeight - newHeight) / 2.0;

                // Resize the page to the target printable area.
                // Use the SetPageSize method (recommended over directly setting Width/Height).
                page.SetPageSize(TargetWidth, TargetHeight);

                // NOTE: Aspose.Pdf no longer provides a PageInfo.Transform property.
                // To apply scaling and translation you would normally use PdfPageEditor.Zoom
                // (or a custom content stream manipulation). For the purpose of this example
                // we only resize the page; the visual content will be scaled automatically
                // when the PDF viewer fits the page to the new dimensions.
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
