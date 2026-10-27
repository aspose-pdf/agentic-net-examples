using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "scanned.pdf";
        const string outputPath = "searchable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the scanned PDF
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page and overlay invisible OCR text
            foreach (Page page in doc.Pages)
            {
                // TODO: replace this placeholder with actual OCR result for the current page
                string ocrText = "Extracted searchable text for this page.";

                // Create a TextFragment containing the OCR text
                TextFragment textFragment = new TextFragment(ocrText);

                // Make the text effectively invisible:
                //   • Set font size to 0
                //   • Use a fully transparent color
                textFragment.TextState.Font = FontRepository.FindFont("Arial");
                textFragment.TextState.FontSize = 0;
                textFragment.TextState.ForegroundColor = Color.FromArgb(0, 0, 0, 0);

                // Position the text at the lower‑left corner of the page
                textFragment.Position = new Position(0, 0);

                // Add the invisible text to the page
                page.Paragraphs.Add(textFragment);
            }

            // Save the searchable PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Searchable PDF saved to '{outputPath}'.");
    }
}
