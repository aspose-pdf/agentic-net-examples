using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "multipart.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 27)
            {
                Console.Error.WriteLine("The document does not contain enough pages for the requested bookmarks.");
                return;
            }

            // Part A bookmark on page 3
            OutlineItemCollection partA = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Part A",
                Action = new GoToAction(doc.Pages[3])
            };
            doc.Outlines.Add(partA);

            // Part B bookmark on page 15
            OutlineItemCollection partB = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Part B",
                Action = new GoToAction(doc.Pages[15])
            };
            doc.Outlines.Add(partB);

            // Part C bookmark on page 27
            OutlineItemCollection partC = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Part C",
                Action = new GoToAction(doc.Pages[27])
            };
            doc.Outlines.Add(partC);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
