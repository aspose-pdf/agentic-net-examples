using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Drawing;      // For Rectangle type (if needed, otherwise fully qualified)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cropped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the amount to trim from each side (in points)
            // Adjust these values as needed for your specific margins
            const double leftMargin   = 50;
            const double bottomMargin = 50;
            const double rightMargin  = 50;
            const double topMargin    = 50;

            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page size
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Calculate new CropBox coordinates
                double llx = leftMargin;                     // lower‑left X
                double lly = bottomMargin;                   // lower‑left Y
                double urx = pageWidth  - rightMargin;       // upper‑right X
                double ury = pageHeight - topMargin;         // upper‑right Y

                // Set the CropBox using a fully qualified Rectangle to avoid ambiguity
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF (saving without explicit SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}