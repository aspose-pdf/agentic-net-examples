using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            Page page = doc.Pages[1];

            // Define a new MediaBox rectangle.
            // Constructor: (llx, lly, urx, ury)
            // Example: lower‑left corner at (50,50), upper‑right at (550,750)
            Aspose.Pdf.Rectangle newMediaBox = new Aspose.Pdf.Rectangle(50, 50, 550, 750);

            // Set the MediaBox – this changes the page size without affecting its content.
            page.MediaBox = newMediaBox;

            // Save the modified PDF (rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}