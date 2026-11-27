using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 35 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 35)
            {
                Console.Error.WriteLine("The document contains fewer than 35 pages.");
                return;
            }

            // Use TextFragmentAbsorber (core API) to capture the textual content of page 35
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.Visit(doc.Pages[35]);

            // Gather extracted texts
            List<string> lines = new List<string>();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (!string.IsNullOrEmpty(fragment.Text))
                {
                    lines.Add(fragment.Text);
                }
            }

            // Write the extracted texts to the output file
            File.WriteAllLines(outputPath, lines);
        }

        Console.WriteLine($"Paragraphs from page 35 have been saved to '{outputPath}'.");
    }
}
