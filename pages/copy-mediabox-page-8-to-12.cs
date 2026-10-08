using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for Rectangle type (if needed)

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
            // Aspose.Pdf uses 1‑based page indexing
            // Ensure the document has at least 12 pages
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("Document does not contain 12 pages.");
                return;
            }

            // Get the MediaBox of page 8
            // MediaBox is an Aspose.Pdf.Rectangle; clone it to avoid reference issues
            Aspose.Pdf.Rectangle sourceBox = doc.Pages[8].MediaBox;
            Aspose.Pdf.Rectangle clonedBox = new Aspose.Pdf.Rectangle(
                sourceBox.LLX,
                sourceBox.LLY,
                sourceBox.URX,
                sourceBox.URY);

            // Apply the cloned MediaBox to page 12
            doc.Pages[12].MediaBox = clonedBox;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"MediaBox copied from page 8 to page 12. Saved as '{outputPath}'.");
    }
}