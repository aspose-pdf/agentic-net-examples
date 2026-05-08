using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the current BleedBox; if not set, fall back to MediaBox
                Aspose.Pdf.Rectangle bleedBox = page.BleedBox ?? page.MediaBox;

                // Example printer specification: expand each side by 5 points
                const double adjustment = 5.0;

                double llx = bleedBox.LLX - adjustment;
                double lly = bleedBox.LLY - adjustment;
                double urx = bleedBox.URX + adjustment;
                double ury = bleedBox.URY + adjustment;

                // Apply the adjusted BleedBox back to the page
                page.BleedBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}