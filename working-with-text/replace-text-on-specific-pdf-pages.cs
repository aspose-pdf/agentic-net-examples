using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path
        const string outputPath = "output.pdf";

        // Text to find and its replacement
        const string searchText = "old phrase";
        const string replaceText = "new phrase";

        // Page range (1‑based indexing)
        const int startPage = 2;
        const int endPage   = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Clamp the range to the actual page count
            int lastPage = Math.Min(endPage, doc.Pages.Count);
            int firstPage = Math.Max(startPage, 1);

            // Iterate over the specified pages
            for (int pageNum = firstPage; pageNum <= lastPage; pageNum++)
            {
                // Create an absorber that searches for the target phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

                // Perform the search on the current page
                doc.Pages[pageNum].Accept(absorber);

                // Replace each found fragment with the new text
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}