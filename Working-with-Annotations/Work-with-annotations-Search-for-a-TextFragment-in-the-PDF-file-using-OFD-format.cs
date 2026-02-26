using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ofd";          // OFD file to be processed
        const string searchPhrase = "Sample Text";     // Text to search for

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the OFD document (Aspose.Pdf auto‑detects the format)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to search for the specified phrase.
            // The absorber will also search inside annotations.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            absorber.TextSearchOptions.SearchInAnnotations = true;

            // Perform the search on all pages.
            doc.Pages.Accept(absorber);

            // Output the results.
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("No occurrences found.");
            }
            else
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s):");
                int index = 1;
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Page number is 1‑based.
                    int pageNumber = fragment.Page.Number;
                    Console.WriteLine($"{index}. Page {pageNumber}: \"{fragment.Text}\"");
                    index++;
                }
            }

            // (Optional) Save the document after any modifications.
            // doc.Save("output.pdf"); // Uncomment if you need to save.
        }
    }
}