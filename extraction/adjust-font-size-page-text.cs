using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 9 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document contains fewer than 9 pages.");
                return;
            }

            // Create a TextFragmentAbsorber to capture fragments with formatting information
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Extract fragments from page 9
            doc.Pages[9].Accept(absorber);

            // Reduce each fragment's font size by 20%
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.TextState != null && fragment.TextState.FontSize > 0)
                {
                    // FontSize is a float; use a float literal to avoid double‑to‑float conversion error
                    fragment.TextState.FontSize = fragment.TextState.FontSize * 0.8f;
                }
            }

            // Concatenate the adjusted text
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                sb.Append(fragment.Text);
            }

            // Write the result to a plain‑text file
            File.WriteAllText(outputPath, sb.ToString());
        }

        Console.WriteLine($"Adjusted text saved to '{outputPath}'.");
    }
}
