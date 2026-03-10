using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;                // Required for Position, etc. (not used here but safe)

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "cropped_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the current page rectangle (considers CropBox if present)
                // The returned rectangle is in user units (1/72 inch)
                Aspose.Pdf.Rectangle currentRect = page.GetPageRect(false);

                // Define the amount to trim from each side (e.g., 50 units)
                const double trimLeft   = 50;
                const double trimBottom = 50;
                const double trimRight  = 50;
                const double trimTop    = 50;

                // Compute new coordinates for the CropBox
                double llx = currentRect.LLX + trimLeft;   // lower‑left X
                double lly = currentRect.LLY + trimBottom; // lower‑left Y
                double urx = currentRect.URX - trimRight;  // upper‑right X
                double ury = currentRect.URY - trimTop;    // upper‑right Y

                // Ensure the new rectangle is valid (non‑negative width/height)
                if (urx > llx && ury > lly)
                {
                    // Set the CropBox; this changes the visible area without altering the underlying content
                    page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                }
                else
                {
                    Console.Error.WriteLine($"Warning: Page {i} crop dimensions are invalid – skipping cropping.");
                }
            }

            // Save the modified document as PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}