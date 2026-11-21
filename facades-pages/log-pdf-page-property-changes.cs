using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";
        const string logPath = "page_modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        // Open the log file inside a using block so it is flushed and closed properly.
        using (StreamWriter log = new StreamWriter(logPath, append: false))
        {
            // Example modification: rotate each page 90 degrees clockwise.
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];

                // Get the current rotation (as degrees) and compute the new rotation.
                int oldRotationDeg = (int)page.Rotate; // Rotation enum maps to 0,90,180,270
                int newRotationDeg = (oldRotationDeg + 90) % 360;
                page.Rotate = (Rotation)newRotationDeg;

                // Log the rotation change.
                log.WriteLine($"Page {i}: Rotation changed from {oldRotationDeg}° to {newRotationDeg}°.");

                // Example modification: change page size to A4 if not already A4.
                // A4 dimensions in points: 595 x 842.
                const double a4Width = 595;
                const double a4Height = 842;

                double oldWidth = page.PageInfo.Width;
                double oldHeight = page.PageInfo.Height;

                if (Math.Abs(oldWidth - a4Width) > 0.1 || Math.Abs(oldHeight - a4Height) > 0.1)
                {
                    // Use SetPageSize to change the page dimensions.
                    page.SetPageSize(a4Width, a4Height);
                    log.WriteLine($"Page {i}: Size changed from {oldWidth}x{oldHeight} to {a4Width}x{a4Height} points.");
                }

                // Example modification using Facades: delete a blank page (if any) via PdfFileEditor.
                // Here we demonstrate the Facades API; actual condition is placeholder.
                // In newer Aspose.PDF versions the image collection is accessed via page.Resources.Images.
                if (page.Paragraphs.Count == 0 &&
                    page.Resources?.Images?.Count == 0 &&
                    page.Annotations.Count == 0)
                {
                    // Collect blank pages and rebuild the document later.
                    // For demonstration we simply log the detection; actual removal would be done with PdfFileEditor.Insert/Extract.
                    log.WriteLine($"Page {i}: Detected as blank – placeholder for deletion using PdfFileEditor.");
                }
            }

            // Save the modified document.
            doc.Save(outputPath);
            log.WriteLine($"Document saved to '{outputPath}'.");
        }

        Console.WriteLine("Page modifications completed and logged.");
    }
}
