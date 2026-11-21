using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cropped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages[1];

            // Define the new MediaBox (crop region) in points (1/72 inch)
            // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            double llx = 50;   // left margin
            double lly = 50;   // bottom margin
            double urx = 500;  // right margin
            double ury = 700;  // top margin

            // Use fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle newMediaBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Apply the custom cropping region without altering page content
            page.MediaBox = newMediaBox;

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"MediaBox updated. Output saved to '{outputPath}'.");
    }
}