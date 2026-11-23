using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_strikeout.pdf";
        const string phrase = "search phrase"; // phrase to find

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            doc.Pages.Accept(absorber);

            // Add a strike‑out annotation for every found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The page that contains the fragment
                Page page = fragment.Page; // <-- use fragment.Page, not PageNumber

                // Bounding rectangle of the fragment
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create the strike‑out annotation
                StrikeOutAnnotation strikeOut = new StrikeOutAnnotation(page, rect)
                {
                    // Red colour (values are 0‑1 range)
                    Color = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0)
                };

                // Add the annotation to the page
                page.Annotations.Add(strikeOut);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strike‑out annotations added and saved to '{outputPath}'.");
    }
}
