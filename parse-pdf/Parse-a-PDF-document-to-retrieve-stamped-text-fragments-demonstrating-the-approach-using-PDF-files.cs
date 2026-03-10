using System;
using System.IO;
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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextFragmentAbsorber to collect all text fragments (including stamps)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Optional: set extraction options for pure formatting (no layout info)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Iterate over the collected text fragments
            Console.WriteLine("Stamped text fragments found in the document:");
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The Text property contains the actual text of the fragment
                // Stamped text appears as regular text fragments, so we simply output it
                Console.WriteLine($"- \"{fragment.Text}\" (Page {fragment.Page.Number})");
            }
        }
    }
}