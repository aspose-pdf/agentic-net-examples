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
        const string srcString  = "Old Text";   // text to replace
        const string destString = "New Text";   // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search for the source text on page 2 to capture its original style
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(srcString);
            doc.Pages[2].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("Source text not found on page 2.");
                // Still save the original document (no changes)
                doc.Save(outputPath);
                return;
            }

            // Take the first occurrence's text state as the style to preserve
            TextFragment firstFragment = absorber.TextFragments[1];
            TextState originalState = new TextState
            {
                Font          = firstFragment.TextState.Font,
                FontSize      = firstFragment.TextState.FontSize,
                FontStyle     = firstFragment.TextState.FontStyle,
                ForegroundColor = firstFragment.TextState.ForegroundColor
            };

            // Edit the PDF using PdfContentEditor (Facades API)
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);                                   // bind the loaded document
                editor.ReplaceText(srcString, 2, destString, originalState); // replace on page 2 preserving style
                editor.Save(outputPath);                               // save the edited PDF
            }
        }

        Console.WriteLine($"Text replaced and saved to '{outputPath}'.");
    }
}