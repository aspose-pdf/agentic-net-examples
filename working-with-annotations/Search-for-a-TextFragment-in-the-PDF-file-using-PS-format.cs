using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the input PostScript file and the output PDF file
        const string inputPsPath  = "input.ps";
        const string outputPdfPath = "output.pdf";

        // Text to search for in the document
        const string searchPhrase = "sample";

        // Verify that the input file exists
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPsPath}");
            return;
        }

        try
        {
            // Load the PostScript file using PsLoadOptions (PS is input‑only)
            using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
            {
                // Create a TextFragmentAbsorber to find all occurrences of the phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

                // Perform the search on the whole document
                doc.Pages.Accept(absorber);

                // Output the found fragments
                Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{searchPhrase}\":");
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"- Page {fragment.Page.Number}: \"{fragment.Text}\"");
                }

                // Save the loaded document as PDF (PS cannot be saved)
                doc.Save(outputPdfPath);
                Console.WriteLine($"Document saved as PDF: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}