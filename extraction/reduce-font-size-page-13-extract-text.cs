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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 13)
            {
                Console.Error.WriteLine("Document has fewer than 13 pages.");
                return;
            }

            // Absorb text fragments from page 13
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[13].Accept(absorber);

            // Reduce each fragment's font size by 15%
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // FontSize is a float, use float arithmetic and explicit cast if needed
                float currentSize = fragment.TextState.FontSize;
                fragment.TextState.FontSize = currentSize * 0.85f; // 15% reduction
            }

            // Collect modified text
            StringBuilder sb = new StringBuilder();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                sb.Append(fragment.Text);
            }

            // Write to output file
            File.WriteAllText(outputPath, sb.ToString());
        }

        Console.WriteLine($"Modified text written to '{outputPath}'.");
    }
}
