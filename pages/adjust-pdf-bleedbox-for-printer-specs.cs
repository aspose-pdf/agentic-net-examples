using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Printer specification: add a 5 mm bleed on each side
            // 1 point = 1/72 inch, 1 inch = 25.4 mm
            const double mmToPt = 72.0 / 25.4;
            double bleedMargin = 5 * mmToPt; // 5 mm expressed in points

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve existing BleedBox; if null, fall back to MediaBox
                Aspose.Pdf.Rectangle currentBleed = page.BleedBox ?? page.MediaBox;

                // Expand the BleedBox outward by the specified margin
                double llx = currentBleed.LLX - bleedMargin;
                double lly = currentBleed.LLY - bleedMargin;
                double urx = currentBleed.URX + bleedMargin;
                double ury = currentBleed.URY + bleedMargin;

                // Assign the adjusted BleedBox (use fully qualified type to avoid ambiguity)
                page.BleedBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"BleedBox values adjusted and saved to '{outputPath}'.");
    }
}