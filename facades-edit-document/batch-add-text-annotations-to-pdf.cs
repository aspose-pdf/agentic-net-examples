using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO to describe an annotation to be added
    class AnnotationDefinition
    {
        public int PageNumber { get; set; }               // 1‑based page index
        public Aspose.Pdf.Rectangle Rectangle { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public Aspose.Pdf.Color Color { get; set; }
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define a batch of annotations
        var annotations = new List<AnnotationDefinition>
        {
            new AnnotationDefinition
            {
                PageNumber = 1,
                Rectangle  = new Aspose.Pdf.Rectangle(100, 700, 300, 750),
                Title      = "Note 1",
                Contents   = "First batch annotation.",
                Color      = Aspose.Pdf.Color.Yellow
            },
            new AnnotationDefinition
            {
                PageNumber = 2,
                Rectangle  = new Aspose.Pdf.Rectangle(150, 500, 350, 550),
                Title      = "Note 2",
                Contents   = "Second batch annotation.",
                Color      = Aspose.Pdf.Color.LightGreen
            },
            // Add more definitions as needed
        };

        // Process the PDF
        using (Document doc = new Document(inputPath))
        {
            foreach (var def in annotations)
            {
                // Validate page number
                if (def.PageNumber < 1 || def.PageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {def.PageNumber} for annotation \"{def.Title}\".");
                    continue;
                }

                // Retrieve the target page (1‑based indexing)
                Page page = doc.Pages[def.PageNumber];

                // Create a TextAnnotation and set its properties
                TextAnnotation textAnnot = new TextAnnotation(page, def.Rectangle)
                {
                    Title    = def.Title,
                    Contents = def.Contents,
                    Color    = def.Color,
                    Open     = true   // open the popup by default
                };

                // Add the annotation to the page's collection
                page.Annotations.Add(textAnnot);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}