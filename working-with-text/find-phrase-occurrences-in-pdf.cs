using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string phrase = "your target phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Search the current page
                doc.Pages[pageNum].Accept(absorber);

                // Output each occurrence found on this page
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // fragment.Rectangle gives the location of the text on the page
                    Console.WriteLine($"Page {pageNum}: \"{fragment.Text}\" at {fragment.Rectangle}");
                }

                // Reset the absorber before processing the next page
                absorber.Reset();
            }
        }
    }
}