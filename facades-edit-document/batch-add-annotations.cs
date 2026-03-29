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

        // Define a batch of annotation specifications
        AnnotationDefinition[] definitions = new AnnotationDefinition[]
        {
            new AnnotationDefinition
            {
                Type = "Text",
                Rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750),
                Contents = "First note",
                Color = Aspose.Pdf.Color.Yellow
            },
            new AnnotationDefinition
            {
                Type = "Link",
                Rect = new Aspose.Pdf.Rectangle(300, 700, 400, 750),
                Url = "https://www.example.com",
                Color = Aspose.Pdf.Color.Blue
            }
        };

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1]; // first page (1‑based indexing)

            foreach (AnnotationDefinition def in definitions)
            {
                if (def.Type == "Text")
                {
                    TextAnnotation textAnn = new TextAnnotation(page, def.Rect);
                    textAnn.Contents = def.Contents;
                    textAnn.Color = def.Color;
                    page.Annotations.Add(textAnn);
                }
                else if (def.Type == "Link")
                {
                    LinkAnnotation linkAnn = new LinkAnnotation(page, def.Rect);
                    linkAnn.Action = new GoToURIAction(def.Url);
                    linkAnn.Color = def.Color;
                    page.Annotations.Add(linkAnn);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }

    private class AnnotationDefinition
    {
        public string Type { get; set; }
        public Aspose.Pdf.Rectangle Rect { get; set; }
        public string Contents { get; set; }
        public string Url { get; set; }
        public Aspose.Pdf.Color Color { get; set; }
    }
}