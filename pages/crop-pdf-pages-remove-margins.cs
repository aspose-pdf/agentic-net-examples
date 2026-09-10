using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cropped_output.pdf";

        // Margins to remove (in points). Adjust as needed.
        const double leftMargin   = 36;   // 0.5 inch
        const double rightMargin  = 36;
        const double topMargin    = 36;
        const double bottomMargin = 36;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page size (MediaBox).
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // Compute new CropBox coordinates by applying margins.
                double llx = mediaBox.LLX + leftMargin;
                double lly = mediaBox.LLY + bottomMargin;
                double urx = mediaBox.URX - rightMargin;
                double ury = mediaBox.URY - topMargin;

                // Set the CropBox. Fully qualified Rectangle avoids ambiguity.
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}