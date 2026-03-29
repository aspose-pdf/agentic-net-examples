using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string phrase = "Your target phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least four pages (1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("Document contains fewer than 4 pages.");
                return;
            }

            Page page = doc.Pages[4];

            // Search for the specific phrase on page 4
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            page.Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"Phrase \"{phrase}\" not found on page 4.");
            }
            else
            {
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;
                    // Create an underline annotation that matches the fragment rectangle
                    UnderlineAnnotation underline = new UnderlineAnnotation(page, rect);
                    underline.Color = Aspose.Pdf.Color.Red;
                    page.Annotations.Add(underline);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation added. Saved to '{outputPath}'.");
    }
}