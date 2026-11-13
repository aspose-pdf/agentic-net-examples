using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "page1.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document has at least one page (pages are 1‑based)
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Use TextAbsorber to extract text from the first page
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[1].Accept(absorber);
            string pageText = absorber.Text ?? string.Empty;

            // Write the extracted text to a UTF‑8 encoded file
            File.WriteAllText(outputTxt, pageText, System.Text.Encoding.UTF8);
        }

        Console.WriteLine($"First page text saved to '{outputTxt}'.");
    }
}