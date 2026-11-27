using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 18 pages
            if (doc.Pages.Count < 18)
            {
                Console.Error.WriteLine("The document does not contain page 18.");
                return;
            }

            // Create a TextAbsorber with Pure formatting mode (clean formatted text)
            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text only from page 18 (1‑based indexing)
            Aspose.Pdf.Page page18 = doc.Pages[18];
            page18.Accept(absorber);

            // Write the extracted text to the output file
            File.WriteAllText(outputTxtPath, absorber.Text);
            Console.WriteLine($"Extracted text from page 18 written to '{outputTxtPath}'.");
        }
    }
}