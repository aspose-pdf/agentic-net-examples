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
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("Document has fewer than 4 pages.");
                return;
            }

            // Use TextFragmentAbsorber (core API) to extract text from page 4.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.Visit(doc.Pages[4]); // page numbers are 1‑based

            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    writer.WriteLine(fragment.Text);
                }
            }
        }

        Console.WriteLine($"Paragraphs from page 4 saved to '{outputPath}'.");
    }
}
