using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the input MHT file and the output PDF file
        const string mhtPath = "input.mht";
        const string outputPdf = "output.pdf";

        // Phrase to search for in the document
        const string searchPhrase = "example";

        // Verify that the MHT file exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Document must be disposed deterministically – use a using block
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Create a TextFragmentAbsorber to search for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Iterate over the found text fragments and display their content and page number
            int idx = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Fragment {idx}: \"{fragment.Text}\" on page {fragment.Page.Number}");
                idx++;
            }

            // Save the (potentially modified) document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine("Processing completed.");
    }
}