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
            // Wild‑card / regular‑expression pattern to match dates in MM/DD/YYYY format
            const string datePattern = @"\b\d{2}/\d{2}/\d{4}\b";

            // Enable regular‑expression search (RegEx = true)
            TextSearchOptions options = new TextSearchOptions(true);

            // Create an absorber that will collect all matching fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(datePattern, options);

            // Apply the absorber to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }

            // Output each found date
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Found date: {fragment.Text}");
            }
        }
    }
}
