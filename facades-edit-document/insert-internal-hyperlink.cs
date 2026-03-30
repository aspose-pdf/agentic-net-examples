using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int sourcePageNumber = 1; // page where the link will appear
        const int targetPageNumber = 2; // page to navigate to

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (sourcePageNumber < 1 || sourcePageNumber > doc.Pages.Count ||
                targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page numbers.");
                return;
            }

            Page sourcePage = doc.Pages[sourcePageNumber];
            Page targetPage = doc.Pages[targetPageNumber];

            // Define the clickable rectangle (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            link.Color = Aspose.Pdf.Color.Blue; // optional visual cue
            // Set the action to navigate to the target page within the same document
            link.Action = new GoToAction(targetPage);
            sourcePage.Annotations.Add(link);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added. Saved to '{outputPath}'.");
    }
}
