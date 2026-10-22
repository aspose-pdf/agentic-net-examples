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
        const int startPage = 2; // inclusive
        const int endPage   = 4; // inclusive

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Document must be wrapped in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Validate page range against the actual page count
                int maxPage = doc.Pages.Count;
                int from = Math.Max(1, startPage);          // pages are 1‑based
                int to   = Math.Min(endPage, maxPage);

                // Iterate over the specified pages and replace text on each page
                for (int i = from; i <= to; i++)
                {
                    // Use TextFragmentAbsorber to find and replace text on a single page
                    var absorber = new TextFragmentAbsorber(searchText);
                    doc.Pages[i].Accept(absorber);

                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.Text = replaceText;
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
