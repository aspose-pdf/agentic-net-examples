using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchWord = "confidential";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Extract all text fragments from the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Case‑insensitive search for the target word
                if (fragment.Text != null &&
                    fragment.Text.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // The page that contains the fragment
                    Page page = fragment.Page;

                    // Bounding rectangle of the fragment
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                    // Create a strike‑out annotation and attach it to the page
                    StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect);
                    page.Annotations.Add(strike);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
