using System;
using System.IO;
using System.Text;
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 15 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The document does not contain page 15.");
                return;
            }

            // Get page 15
            Page page15 = doc.Pages[15];

            // Absorb text fragments from the page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            page15.Accept(absorber);

            // Reduce each fragment's font size by 10%
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // FontSize is a float; use a float literal to avoid double → float conversion error
                fragment.TextState.FontSize = fragment.TextState.FontSize * 0.9f;
            }

            // Collect the (unchanged) text after modification
            StringBuilder sb = new StringBuilder();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                sb.Append(fragment.Text);
            }

            // Write the extracted text to the output file
            File.WriteAllText(outputPath, sb.ToString());
        }

        Console.WriteLine($"Modified text saved to '{outputPath}'.");
    }
}
