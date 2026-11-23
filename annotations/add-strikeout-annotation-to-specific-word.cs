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
        const string wordToStrike = "targetWord"; // word to be struck out

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for the specific word using TextFragmentAbsorber
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(wordToStrike);
            // Accept the absorber on all pages
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment and add a StrikeOutAnnotation
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The fragment provides its bounding rectangle (Aspose.Pdf.Rectangle)
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Get the page that contains the fragment. "fragment.Page" returns a Page object.
                Page page = fragment.Page;

                // Create the strike‑out annotation for the page containing the fragment
                StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect);

                // Optional: set appearance properties
                strike.Color = Aspose.Pdf.Color.Red; // strike colour
                strike.Opacity = 0.5;                // semi‑transparent
                strike.Title = "StrikeOut";        // tooltip title

                // Add the annotation to the page
                page.Annotations.Add(strike);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strike‑out annotation added and saved to '{outputPath}'.");
    }
}
