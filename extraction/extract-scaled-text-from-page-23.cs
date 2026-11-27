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

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Verify that page 23 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 23)
            {
                Console.Error.WriteLine("The document does not contain page 23.");
                return;
            }

            // Create a TextAbsorber with pure formatting mode
            Aspose.Pdf.Text.TextAbsorber absorber = new Aspose.Pdf.Text.TextAbsorber();
            absorber.ExtractionOptions = new Aspose.Pdf.Text.TextExtractionOptions(
                Aspose.Pdf.Text.TextExtractionOptions.TextFormattingMode.Pure);

            // Configure the scale factor for extraction (0.5)
            absorber.ExtractionOptions.ScaleFactor = 0.5;

            // Extract text from page 23 only
            Aspose.Pdf.Page page23 = doc.Pages[23];
            page23.Accept(absorber);

            // Write the extracted text to the output file
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}