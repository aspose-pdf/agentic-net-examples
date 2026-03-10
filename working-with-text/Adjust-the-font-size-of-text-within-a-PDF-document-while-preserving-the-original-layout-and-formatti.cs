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
        const float  newSize    = 14f; // desired font size

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Absorb all text fragments on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Prepare the facade for editing (uses Aspose.Pdf.Facades)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Iterate over each fragment and replace it with the same text,
            // applying a new TextState that only changes the font size
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Preserve existing font, style and color, change only size
                TextState ts = new TextState
                {
                    Font       = fragment.TextState.Font,
                    FontStyle  = fragment.TextState.FontStyle,
                    ForegroundColor = fragment.TextState.ForegroundColor,
                    FontSize   = newSize
                };

                // Replace the fragment text with itself using the new TextState
                // Page numbers are 1‑based in Aspose.Pdf
                editor.ReplaceText(fragment.Text, fragment.Page.Number, fragment.Text, ts);
            }

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Font size adjusted and saved to '{outputPath}'.");
    }
}