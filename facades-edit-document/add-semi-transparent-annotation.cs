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
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Semi-transparent annotation",
                Color = Aspose.Pdf.Color.Yellow,
                Opacity = 0.6
            };

            page.Annotations.Add(annotation);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}