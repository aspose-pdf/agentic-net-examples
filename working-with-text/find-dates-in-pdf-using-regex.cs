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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Regular expression to match dates in MM/DD/YYYY format
            string datePattern = @"\b\d{2}/\d{2}/\d{4}\b";

            // Search each page individually because the newer API does not expose Document.FindText
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Use the constructor that accepts a pattern and TextSearchOptions (regex enabled)
                var absorber = new TextFragmentAbsorber(datePattern, new TextSearchOptions(true));

                // Apply the absorber to the current page
                doc.Pages[pageIndex].Accept(absorber);

                // Output each found date together with its page number
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Page {pageIndex}: {fragment.Text}");
                }
            }
        }
    }
}
