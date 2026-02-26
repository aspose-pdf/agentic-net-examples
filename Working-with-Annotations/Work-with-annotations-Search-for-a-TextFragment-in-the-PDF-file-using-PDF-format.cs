using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string pattern    = "Aspose"; // search term or regex pattern

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Optional: bind the document to a PdfContentEditor (facade API) – not required for search,
            // but demonstrates usage of Aspose.Pdf.Facades as requested.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Create a TextSearchOptions instance that enables regular‑expression mode
            TextSearchOptions searchOptions = new TextSearchOptions(true);

            // Build a TextFragmentAbsorber that searches using the regex pattern
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(
                new Regex(pattern, RegexOptions.IgnoreCase),
                searchOptions);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Iterate over all found text fragments and output their details
            int idx = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Fragment {idx}:");
                Console.WriteLine($"  Text    : {fragment.Text}");
                Console.WriteLine($"  Position: X={fragment.Position.XIndent}, Y={fragment.Position.YIndent}");
                Console.WriteLine($"  Font    : {fragment.TextState.Font.FontName}, Size={fragment.TextState.FontSize}");
                Console.WriteLine($"  Color   : {fragment.TextState.ForegroundColor}");
                idx++;
            }

            // Save the (unchanged) document – no modifications were made
            doc.Save(outputPath);
        }

        Console.WriteLine($"Search completed. Output saved to '{outputPath}'.");
    }
}