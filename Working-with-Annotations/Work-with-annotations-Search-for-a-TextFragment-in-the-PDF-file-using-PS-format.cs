using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPs  = "output.ps";          // result in PostScript format
        const string searchPhrase = "Aspose";          // text to search for

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextFragmentAbsorber that looks for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Search the whole document (pages are 1‑based)
            doc.Pages.Accept(absorber);

            // Output information about each found fragment
            int index = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Position is given by the fragment's rectangle (lower‑left x/y)
                Console.WriteLine($"Fragment {index}:");
                Console.WriteLine($"  Text      : {fragment.Text}");
                Console.WriteLine($"  Page      : {fragment.Page.Number}");
                Console.WriteLine($"  Rectangle : {fragment.Rectangle}");
                index++;
            }

            // Save the (unchanged) document as PostScript using explicit save options
            PsSaveOptions psOptions = new PsSaveOptions();
            doc.Save(outputPs, psOptions);
        }

        Console.WriteLine($"Search completed. PostScript saved to '{outputPs}'.");
    }
}