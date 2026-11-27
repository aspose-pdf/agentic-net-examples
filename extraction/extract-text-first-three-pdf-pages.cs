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
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // StringBuilder to concatenate text from pages 1‑3
            StringBuilder sb = new StringBuilder();

            // Pages are 1‑based; stop if the document has fewer than 3 pages
            int maxPage = Math.Min(3, doc.Pages.Count);
            for (int i = 1; i <= maxPage; i++)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };
                doc.Pages[i].Accept(absorber);

                // Append extracted text and a newline separator
                sb.AppendLine(absorber.Text);
            }

            // Write concatenated text to a .txt file
            File.WriteAllText(outputTxt, sb.ToString());
        }

        Console.WriteLine($"Text from pages 1‑3 saved to '{outputTxt}'.");
    }
}