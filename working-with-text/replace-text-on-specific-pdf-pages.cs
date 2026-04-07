using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Output PDF file
        const string outputPath = "output.pdf";

        // Text to find and its replacement
        const string searchText = "old phrase";
        const string replaceText = "new phrase";

        // Page range (1‑based indexing)
        const int startPage = 2; // inclusive
        const int endPage   = 5; // inclusive

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Validate page range
        if (startPage < 1 || endPage < startPage)
        {
            Console.Error.WriteLine("Invalid page range.");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure the requested range does not exceed the document page count
                int maxPage = Math.Min(endPage, doc.Pages.Count);

                // Iterate through the required pages and replace the text on each page
                for (int pageNum = startPage; pageNum <= maxPage; pageNum++)
                {
                    // Create a new absorber for the current page
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

                    // Accept the absorber only on the current page
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
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
