using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputCgmPath = "input.cgm";
        const string searchPhrase = "Sample Text";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        // Load CGM file (CGM is input‑only, so we load it as a PDF document)
        using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
        {
            // Create a TextFragmentAbsorber to search for the desired phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Perform the search on all pages
            doc.Pages.Accept(absorber);

            // Output the results
            if (absorber.TextFragments.Count > 0)
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{searchPhrase}\":");
                int index = 1;
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"[{index}] Page {fragment.Page.Number}, Text: \"{fragment.Text}\"");
                    index++;
                }
            }
            else
            {
                Console.WriteLine($"No occurrences of \"{searchPhrase}\" were found.");
            }
        }
    }
}