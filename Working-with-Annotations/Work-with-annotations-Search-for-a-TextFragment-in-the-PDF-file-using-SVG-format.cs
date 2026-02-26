using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputSvg = "output.svg";     // SVG result
        const string searchPattern = @"\bexample\b"; // regex pattern to find the word "example"

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Enable regular‑expression search
                TextSearchOptions searchOpts = new TextSearchOptions(true);

                // Create a TextFragmentAbsorber that searches using the regex pattern
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPattern, searchOpts);

                // Apply the absorber to all pages
                doc.Pages.Accept(absorber);

                // Output information about each found fragment
                int index = 1;
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Fragment {index}:");
                    Console.WriteLine($"  Text   : {fragment.Text}");
                    Console.WriteLine($"  Page   : {fragment.Page.Number}");
                    Console.WriteLine($"  X      : {fragment.Position.XIndent}");
                    Console.WriteLine($"  Y      : {fragment.Position.YIndent}");
                    Console.WriteLine($"  Font   : {fragment.TextState.Font.FontName}");
                    Console.WriteLine($"  Size   : {fragment.TextState.FontSize}");
                    Console.WriteLine($"  Color  : {fragment.TextState.ForegroundColor}");
                    index++;
                }

                // Save the (possibly unchanged) document as SVG.
                // SVG conversion requires GDI+; wrap in try‑catch for non‑Windows platforms.
                try
                {
                    doc.Save(outputSvg, new SvgSaveOptions());
                    Console.WriteLine($"SVG saved to '{outputSvg}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("SVG conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}