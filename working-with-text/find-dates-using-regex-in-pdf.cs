using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Regular‑expression pattern that matches dates in the format MM/DD/YYYY
            const string datePattern = @"\b\d{2}/\d{2}/\d{4}\b";

            // Enable regular‑expression search
            TextSearchOptions searchOptions = new TextSearchOptions(true);

            // Create a TextFragmentAbsorber with the regex pattern and options
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(datePattern, searchOptions);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Iterate over the found fragments and display the dates and their page numbers
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Found date: {fragment.Text} on page {fragment.Page.Number}");
            }
        }
    }
}
