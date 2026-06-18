using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "first_page.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Extract text from the first page using TextAbsorber (text-extraction-use-textabsorber-not-page-extracttext)
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[1].Accept(absorber); // page indexing is 1‑based (page-indexing-one-based)

            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text to a UTF‑8 encoded file
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}