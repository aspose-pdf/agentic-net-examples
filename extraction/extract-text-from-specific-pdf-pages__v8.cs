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

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // StringBuilder to accumulate text from pages 23 and 24
            StringBuilder mergedText = new StringBuilder();

            // Pages are 1‑based; iterate over the required pages
            for (int pageNumber = 23; pageNumber <= 24; pageNumber++)
            {
                // Ensure the page exists
                if (pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
                    continue;
                }

                // Use TextAbsorber (core API) to extract the page's text
                TextAbsorber absorber = new TextAbsorber();
                absorber.Visit(pdfDoc.Pages[pageNumber]);

                // Append the extracted text (preserves line breaks) to the builder
                mergedText.AppendLine(absorber.Text);
            }

            // Write the merged text to a plain‑text file
            File.WriteAllText(outputTxtPath, mergedText.ToString());

            Console.WriteLine($"Extracted text saved to '{outputTxtPath}'.");
        }
    }
}
