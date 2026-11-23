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
        const string outputPath = "output_strikeout.pdf";
        const string searchText = "target phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use TextFragmentAbsorber to locate the desired text fragment
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            absorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The fragment already knows its page
                Page page = fragment.Page;

                // The fragment provides its bounding rectangle (Aspose.Pdf.Rectangle)
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create a StrikeOutAnnotation at the fragment's rectangle
                StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect);
                // Optional: set appearance properties
                strike.Color = Aspose.Pdf.Color.Red;
                strike.Opacity = 0.5;

                // Add the annotation to the page
                page.Annotations.Add(strike);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strike‑out annotations added and saved to '{outputPath}'.");
    }
}
