using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcString = "OldText";   // text to replace
        const string destString = "NewText"; // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------------------------
            // 1. Find the first occurrence of the source text on page 2 and capture
            //    its original TextState (font, size, style, colour, etc.).
            // -------------------------------------------------------------------
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(srcString);
            // Page numbers are 1‑based, so page 2 is accessed via index 2.
            doc.Pages[2].Accept(absorber);

            if (absorber.TextFragments.Count > 0)
            {
                // TextFragmentCollection is zero‑based, so we take the first fragment at index 0.
                TextFragment fragment = absorber.TextFragments[0];
                TextState originalState = new TextState
                {
                    Font = fragment.TextState.Font,
                    FontSize = fragment.TextState.FontSize,
                    FontStyle = fragment.TextState.FontStyle,
                    ForegroundColor = fragment.TextState.ForegroundColor
                };

                // -------------------------------------------------------------------
                // 2. Replace the text while applying the captured style.
                // -------------------------------------------------------------------
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    editor.BindPdf(doc);
                    // ReplaceText(string oldValue, int pageNumber, string newValue, TextState textState)
                    editor.ReplaceText(srcString, 2, destString, originalState);
                }
            }
            else
            {
                Console.WriteLine($"Source text \"{srcString}\" not found on page 2.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}
