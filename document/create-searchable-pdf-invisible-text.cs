using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "scanned_input.pdf";
        const string outputPdfPath = "searchable_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the scanned PDF. Each page already contains an image.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages and overlay invisible text.
            // In a real scenario the OCR text would be obtained from an OCR engine.
            // Here we use a placeholder string for demonstration.
            const string placeholderOcrText = "Sample OCR text extracted from the scanned image.";

            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a TextFragment with the OCR text.
                TextFragment textFragment = new TextFragment(placeholderOcrText);

                // Position the fragment at the lower‑left corner of the page.
                // Adjust as needed to match the actual image location.
                textFragment.Position = new Position(0, 0);

                // Make the text invisible but searchable.
                textFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                textFragment.TextState.FontSize = 12;
                textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Transparent;
                // RenderingMode.Invisible is not available in older versions; using transparent color is sufficient.
                // textFragment.TextState.RenderingMode = RenderingMode.Invisible;

                // Add the fragment to the page.
                page.Paragraphs.Add(textFragment);
            }

            // Save the resulting PDF. No special SaveOptions are required.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Searchable PDF saved to '{outputPdfPath}'.");
    }
}
