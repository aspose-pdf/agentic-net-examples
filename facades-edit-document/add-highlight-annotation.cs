using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "highlight";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document has less than 3 pages.");
                return;
            }

            // Search for the specified text on page 3
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            absorber.TextSearchOptions = new TextSearchOptions(true);
            doc.Pages[3].Accept(absorber);
            TextFragmentCollection fragments = absorber.TextFragments;

            if (fragments.Count == 0)
            {
                Console.Error.WriteLine($"Text \"{searchText}\" not found on page 3.");
            }
            else
            {
                // Use the first occurrence of the text
                TextFragment fragment = fragments[1];
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[3], rect);
                highlight.Color = Aspose.Pdf.Color.Yellow;
                highlight.Contents = $"Highlighted \"{searchText}\"";

                doc.Pages[3].Annotations.Add(highlight);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}