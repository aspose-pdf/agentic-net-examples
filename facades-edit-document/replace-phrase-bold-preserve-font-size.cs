using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcPhrase = "old phrase";
        const string destPhrase = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Find the original font size of the source phrase (first occurrence)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(srcPhrase);
            // Search on the first page (page indexing is 1‑based)
            doc.Pages[1].Accept(absorber);

            // FontSize in Aspose.Pdf.Text.TextState is a float, so use float here
            float originalFontSize = 12f; // fallback size
            if (absorber.TextFragments.Count > 0)
            {
                // Use the font size from the first text fragment's TextState
                originalFontSize = absorber.TextFragments[1].TextState.FontSize;
            }

            // Create a TextState that keeps the original font size and applies Bold style
            TextState textState = new TextState
            {
                FontSize = originalFontSize,
                FontStyle = FontStyles.Bold
            };

            // Use PdfContentEditor to replace the phrase and apply the TextState
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);
            // thePage = 0 means replace on all pages
            editor.ReplaceText(srcPhrase, 0, destPhrase, textState);

            // Save the modified PDF (using rule: document disposal with using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
