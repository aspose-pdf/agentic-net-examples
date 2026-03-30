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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document does not contain a second page.");
                return;
            }

            Page page = doc.Pages[2];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Action = new GoToURIAction("https://example.com");
            link.Color = Aspose.Pdf.Color.Blue;
            page.Annotations.Add(link);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
    }
}
