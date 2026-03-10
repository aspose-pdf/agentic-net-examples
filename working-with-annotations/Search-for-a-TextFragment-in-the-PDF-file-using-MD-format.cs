using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        // Regex pattern to match Markdown headings (e.g., "# Title")
        const string markdownPattern = "^#\\s.*";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that uses a regular expression to find markdown headings
            var absorber = new TextFragmentAbsorber(
                new System.Text.RegularExpressions.Regex(markdownPattern),
                new TextSearchOptions(true) // enable regex search
            );

            // Search all pages of the document
            doc.Pages.Accept(absorber);

            // Output the search results
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("No Markdown headings were found in the document.");
            }
            else
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} Markdown heading(s):");
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Display the found text and its location
                    Console.WriteLine($"Text   : \"{fragment.Text}\"");
                    Console.WriteLine($"Page   : {fragment.Page.Number}");
                    Console.WriteLine($"X Pos  : {fragment.Position.XIndent}");
                    Console.WriteLine($"Y Pos  : {fragment.Position.YIndent}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }
}