using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcText    = "Old Text";   // text to replace
        const string destText   = "New Text";   // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first occurrence of the source text on page 2
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(srcText);
            // Page indexing is 1‑based; page 2 is the target page
            doc.Pages[2].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("Source text not found on page 2.");
                doc.Save(outputPath); // save unchanged document
                return;
            }

            // Retrieve the original TextState (font, size, color, etc.)
            TextFragment firstFragment = absorber.TextFragments[1];
            TextState originalState = firstFragment.TextState;

            // Use PdfContentEditor to replace the text while preserving style
            using (PdfContentEditor editor = new PdfContentEditor(doc))
            {
                // Replace on page 2 (thePage parameter is 2; 0 means all pages)
                editor.ReplaceText(srcText, 2, destText, originalState);
                // Save the modified document
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Text replaced and saved to '{outputPath}'.");
    }
}