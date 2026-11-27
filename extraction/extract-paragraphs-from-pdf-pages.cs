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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // TextAbsorber extracts the textual content of a page. In Aspose.Pdf the
            // ParagraphAbsorber class is not available in older versions, therefore we
            // use TextAbsorber which returns the page text as a single string – effectively
            // the concatenation of all paragraphs on that page.
            string ExtractPageText(int pageNumber)
            {
                var absorber = new TextAbsorber();
                absorber.Visit(pdfDoc.Pages[pageNumber]);
                return absorber.Text ?? string.Empty;
            }

            // Extract text from pages 20 and 21
            string page20Text = ExtractPageText(20);
            string page21Text = ExtractPageText(21);

            // Combine the texts from both pages
            string combinedText = page20Text + Environment.NewLine + page21Text;

            // Save the combined text to a plain text file
            File.WriteAllText(outputTxtPath, combinedText);
        }

        Console.WriteLine($"Paragraphs from pages 20 and 21 saved to '{outputTxtPath}'.");
    }
}
