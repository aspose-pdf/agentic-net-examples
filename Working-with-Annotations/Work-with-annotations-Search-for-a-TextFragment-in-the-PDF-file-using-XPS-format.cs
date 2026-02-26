using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputXps = "output.xps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Create a TextFragmentAbsorber to search for the phrase "example"
            // Enable regular‑expression mode (true) via TextSearchOptions
            Aspose.Pdf.Text.TextFragmentAbsorber absorber = new Aspose.Pdf.Text.TextFragmentAbsorber("example");
            absorber.TextSearchOptions = new Aspose.Pdf.Text.TextSearchOptions(true);

            // Perform the search on all pages
            doc.Pages.Accept(absorber);

            // Iterate over the found fragments and display basic info
            int idx = 1;
            foreach (Aspose.Pdf.Text.TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Fragment {idx}: \"{fragment.Text}\" found on page {fragment.Page.Number}");
                idx++;
            }

            // Save the document in XPS format (requires explicit XpsSaveOptions)
            Aspose.Pdf.XpsSaveOptions xpsOptions = new Aspose.Pdf.XpsSaveOptions();
            doc.Save(outputXps, xpsOptions);
        }

        Console.WriteLine($"XPS file saved to '{outputXps}'.");
    }
}