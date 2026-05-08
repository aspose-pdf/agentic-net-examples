using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Page range (1‑based indexing)
        int startPage = 2; // first page to process
        int endPage   = 4; // last page to process

        // Text to find and its replacement
        const string oldText = "Hello";
        const string newText = "Hi";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Clamp the page range to the actual document size
            int totalPages = doc.Pages.Count;
            if (startPage < 1) startPage = 1;
            if (endPage > totalPages) endPage = totalPages;
            if (startPage > endPage) return; // nothing to do

            // Iterate over each page in the specified range
            for (int pageNum = startPage; pageNum <= endPage; pageNum++)
            {
                // Create an absorber that searches for the target phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);

                // Perform the search on the current page
                doc.Pages[pageNum].Accept(absorber);

                // Replace every occurrence found on this page
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = newText;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced \"{oldText}\" with \"{newText}\" on pages {startPage}-{endPage} and saved to \"{outputPath}\".");
    }
}