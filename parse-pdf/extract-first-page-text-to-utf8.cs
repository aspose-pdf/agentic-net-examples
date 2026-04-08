using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber();

            // Extract text from the first page (Aspose.Pdf uses 1‑based indexing)
            doc.Pages[1].Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the text to a UTF‑8 encoded file
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}