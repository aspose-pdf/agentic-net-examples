using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Text to find and its replacement
        const string searchText  = "old phrase";
        const string replaceText = "new phrase";

        // Define the page range (inclusive). Aspose.Pdf uses 1‑based indexing.
        const int startPage = 2;
        const int endPage   = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested range is within the document bounds.
            int maxPage = doc.Pages.Count;
            int from = Math.Max(1, startPage);
            int to   = Math.Min(endPage, maxPage);

            // Iterate over the specified pages.
            for (int i = from; i <= to; i++)
            {
                Page page = doc.Pages[i];

                // Create an absorber that searches for the target phrase.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

                // Perform the search on the current page.
                page.Accept(absorber);

                // Replace each found fragment with the new text.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}