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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 5 pages (Aspose.Pdf uses 1‑based indexing)
            if (pdfDoc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            // Create a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber on page 5 only
            pdfDoc.Pages[5].Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text ?? string.Empty;

            // Write the text to the output file
            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                writer.Write(extractedText);
            }

            Console.WriteLine($"Text from page 5 saved to '{outputTxtPath}'.");
        }
    }
}