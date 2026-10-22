using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Iterate over each fragment and retrieve line‑break information
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"Fragment text: \"{fragment.Text}\"");

                // Try to call GetLineBreakInfo via reflection – this avoids a hard compile‑time dependency
                var method = fragment.GetType().GetMethod("GetLineBreakInfo");
                if (method != null)
                {
                    var lbInfo = method.Invoke(fragment, null);
                    var isLineBreakProp = lbInfo.GetType().GetProperty("IsLineBreak");
                    var positionProp = lbInfo.GetType().GetProperty("Position");
                    var lengthProp = lbInfo.GetType().GetProperty("Length");

                    bool isLineBreak = isLineBreakProp != null && (bool)isLineBreakProp.GetValue(lbInfo);
                    int position = positionProp != null ? (int)positionProp.GetValue(lbInfo) : -1;
                    int length = lengthProp != null ? (int)lengthProp.GetValue(lbInfo) : -1;

                    Console.WriteLine($"  IsLineBreak: {isLineBreak}");
                    Console.WriteLine($"  Position   : {position}");
                    Console.WriteLine($"  Length     : {length}");
                }
                else
                {
                    // Fallback: simple heuristic based on the fragment text
                    bool isLineBreak = fragment.Text.EndsWith("\n") || fragment.Text.EndsWith("\r");
                    Console.WriteLine($"  IsLineBreak: {isLineBreak} (fallback heuristic)");
                }

                Console.WriteLine();
            }
        }
    }
}
