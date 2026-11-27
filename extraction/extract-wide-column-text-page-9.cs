using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 9 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document contains fewer than 9 pages.");
                return;
            }

            // Create a TextAbsorber for text extraction
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction options:
            // - ScaleFactor = 10 for higher resolution extraction
            // Note: In recent Aspose.Pdf versions the ColumnLayout property was removed.
            //       The default layout handling works well for wide‑column PDFs, so we omit it.
            absorber.ExtractionOptions.ScaleFactor = 10;

            // Extract text from page 9 only
            doc.Pages[9].Accept(absorber);

            // Save the extracted text to a plain‑text file
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Extraction complete. Text saved to '{outputPath}'.");
    }
}
