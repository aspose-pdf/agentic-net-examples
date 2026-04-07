using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Regular expression for dates in MM/DD/YYYY format
            Regex dateRegex = new Regex(@"\b\d{2}/\d{2}/\d{4}\b");

            // Enable regular‑expression search
            TextSearchOptions searchOptions = new TextSearchOptions(true);

            // Create an absorber that uses the regex and the search options
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(dateRegex, searchOptions);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Output each found date and its page number
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Found date \"{fragment.Text}\" on page {fragment.Page.Number}");
            }

            // Save the (unchanged) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Results saved to \"{outputPath}\".");
    }
}
