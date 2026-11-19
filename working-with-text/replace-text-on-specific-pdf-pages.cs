using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string outputPath  = "output.pdf";
        const string searchText  = "old text";
        const string replaceText = "new text";

        // Define the page range (inclusive) where replacement should occur
        const int startPage = 2;
        const int endPage   = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Clamp the requested range to the actual number of pages
            int maxPage = doc.Pages.Count;
            int from    = Math.Max(1, startPage);          // Aspose.Pdf uses 1‑based indexing
            int to      = Math.Min(endPage, maxPage);

            // Iterate over each page in the range and replace text only on that page
            for (int i = from; i <= to; i++)
            {
                // Use TextFragmentAbsorber to locate the text on the specific page
                var absorber = new TextFragmentAbsorber(searchText);
                doc.Pages[i].Accept(absorber);

                // Replace each found fragment's text with the new value
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}
