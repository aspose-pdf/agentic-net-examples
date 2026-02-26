using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define a regular expression that matches Markdown headings (e.g., "# Title")
            // TextSearchOptions(true) enables regular‑expression search
            var searchOptions = new TextSearchOptions(true);
            var headingRegex = new Regex(@"^#\s.*", RegexOptions.Multiline);

            // Create a TextFragmentAbsorber that searches for the Markdown heading pattern
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(headingRegex, searchOptions);

            // Perform the search on the whole document
            absorber.Visit(doc);

            // Output the results
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("No Markdown headings were found.");
            }
            else
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} Markdown heading(s):");
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The Page property tells us on which page the fragment was found
                    Console.WriteLine($"Page {fragment.Page.Number}: \"{fragment.Text}\"");
                }
            }

            // (Optional) Save the document if any modifications were made
            // doc.Save("output.pdf");
        }
    }
}