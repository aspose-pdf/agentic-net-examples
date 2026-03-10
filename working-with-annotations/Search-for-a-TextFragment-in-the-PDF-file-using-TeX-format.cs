using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the input PDF file
        const string inputPath = "input.pdf";

        // TeX formatted phrase to search for (example: a LaTeX equation)
        const string texPhrase = @"\alpha + \beta = \gamma";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the TeX phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(texPhrase);

            // Perform the search on the whole document
            doc.Pages.Accept(absorber);

            // Check if any matches were found
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("No TeX fragments were found.");
            }
            else
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} TeX fragment(s):");

                // Iterate over all found TextFragment objects
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Output the fragment text and its page number
                    Console.WriteLine($"Page {fragment.Page.Number}: \"{fragment.Text}\"");
                }
            }
        }
    }
}