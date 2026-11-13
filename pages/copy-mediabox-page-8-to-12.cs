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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 12 pages (pages are 1‑based)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("The document must contain at least 12 pages.");
                return;
            }

            // Retrieve the MediaBox from page 8
            Aspose.Pdf.Rectangle sourceBox = doc.Pages[8].MediaBox;

            // Apply the same dimensions to page 12
            doc.Pages[12].MediaBox = new Aspose.Pdf.Rectangle(
                sourceBox.LLX, sourceBox.LLY, sourceBox.URX, sourceBox.URY);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"MediaBox copied from page 8 to page 12. Saved as '{outputPath}'.");
    }
}