using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Text to find and its replacement
        const string searchText  = "old phrase";
        const string replaceText = "new phrase";

        // Define the page range (1‑based indexing)
        const int startPage = 2; // inclusive
        const int endPage   = 5; // inclusive

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested range is within the document bounds
            int pageCount = doc.Pages.Count;
            int first = Math.Max(1, startPage);
            int last  = Math.Min(pageCount, endPage);

            // Iterate over the specified pages
            for (int i = first; i <= last; i++)
            {
                // Create an absorber that searches for the target phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

                // Perform the search on the current page
                doc.Pages[i].Accept(absorber);

                // Replace each found occurrence with the new text
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}