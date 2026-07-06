using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Extension method to provide line‑break information when the native API is unavailable.
public static class TextFragmentExtensions
{
    /// <summary>
    /// Returns a simple representation of line‑break information for a <see cref="TextFragment"/>.
    /// In newer Aspose.Pdf versions this information is available via TextFragment.GetLineBreakInfo().
    /// This fallback uses the fragment's Y‑coordinate to infer line placement.
    /// </summary>
    public static string GetLineBreakInfo(this TextFragment fragment)
    {
        // The Y‑indent (vertical position) can be used as a proxy for line identification.
        // A more sophisticated implementation could compare with previous fragments.
        return $"YIndent = {fragment.Position.YIndent}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (optional command‑line argument)
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Absorb all text fragments on the first page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[1].Accept(absorber);

            int fragmentIndex = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Retrieve line‑break information (fallback extension method if native API is missing)
                var lineBreakInfo = fragment.GetLineBreakInfo();

                // Log details to the console
                Console.WriteLine($"Fragment #{fragmentIndex}");
                Console.WriteLine($"  Text          : {fragment.Text}");
                Console.WriteLine($"  Position X    : {fragment.Position.XIndent}");
                Console.WriteLine($"  Position Y    : {fragment.Position.YIndent}");
                Console.WriteLine($"  LineBreakInfo : {lineBreakInfo}");
                fragmentIndex++;
            }
        }
    }
}
