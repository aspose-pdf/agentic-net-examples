using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";
        const int targetPageNumber = 19; // 1‑based page index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify that the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Use TextAbsorber (the core API) to extract the textual content of the page.
            // ParagraphAbsorber is not available in the referenced version, and TextAbsorber provides the same result.
            TextAbsorber textAbsorber = new TextAbsorber();
            textAbsorber.Visit(pdfDoc.Pages[targetPageNumber]);

            // The Text property contains the whole page text, including line‑breaks that separate paragraphs.
            string pageText = textAbsorber.Text ?? string.Empty;

            // Write the extracted text to the output file.
            File.WriteAllText(outputTxtPath, pageText, Encoding.UTF8);

            Console.WriteLine($"Extracted text from page {targetPageNumber} saved to '{outputTxtPath}'.");
        }
    }
}
