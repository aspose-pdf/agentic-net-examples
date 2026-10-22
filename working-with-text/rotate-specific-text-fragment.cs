using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";
        const string targetText = "Sample Text"; // text to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Variables to hold the page and fragment once we find the text
            Page targetPage = null;
            TextFragment foundFragment = null;

            // Iterate through pages until the fragment is found – this avoids the missing PageNumber property
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(targetText);
                absorber.TextSearchOptions = new TextSearchOptions(true);
                page.Accept(absorber);

                if (absorber.TextFragments.Count > 0)
                {
                    // First occurrence on this page
                    foundFragment = absorber.TextFragments[1];
                    targetPage = page;
                    break;
                }
            }

            if (foundFragment == null || targetPage == null)
            {
                Console.WriteLine($"Text \"{targetText}\" not found.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Create a new fragment with the same text, positioned at the original location
            TextFragment rotatedFragment = new TextFragment(targetText);
            rotatedFragment.Position = new Position(foundFragment.Position.XIndent, foundFragment.Position.YIndent);
            rotatedFragment.TextState.Rotation = 45; // rotate 45 degrees

            // Add the rotated fragment to the page (it will appear on top of the original)
            targetPage.Paragraphs.Add(rotatedFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text saved to '{outputPath}'.");
    }
}
