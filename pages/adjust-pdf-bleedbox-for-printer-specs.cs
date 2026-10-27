using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bleed_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Retrieve the current BleedBox (a Rectangle)
                Aspose.Pdf.Rectangle bleed = doc.Pages[i].BleedBox;

                // Example adjustment: expand each side by 5 points to meet printer specs
                // Ensure coordinates stay non‑negative
                double left   = Math.Max(0, bleed.LLX - 5);
                double bottom = Math.Max(0, bleed.LLY - 5);
                double right  = bleed.URX + 5;
                double top    = bleed.URY + 5;

                // Apply the adjusted BleedBox back to the page
                doc.Pages[i].BleedBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"BleedBox values adjusted and saved to '{outputPath}'.");
    }
}