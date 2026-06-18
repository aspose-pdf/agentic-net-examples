using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    // Simple definition for an annotation to be added
    private class AnnotationDef
    {
        public int PageNumber;               // 1‑based page index
        public Aspose.Pdf.Rectangle Rect;    // Position and size of the annotation
        public string Title;                 // Title (for markup annotations)
        public string Contents;              // Text shown in the annotation
        public Color Color;                  // Color of the annotation
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
        AnnotationDef[] batch = new AnnotationDef[]
        {
            new AnnotationDef
            {
                PageNumber = 1,
                Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550),
                Title = "Note 1",
                Contents = "First batch annotation",
                Color = Aspose.Pdf.Color.Yellow
            },
            new AnnotationDef
            {
                PageNumber = 2,
                Rect = new Aspose.Pdf.Rectangle(150, 400, 350, 450),
                Title = "Note 2",
                Contents = "Second batch annotation",
                Color = Aspose.Pdf.Color.LightGreen
            },
            // Add more definitions as needed
        };

        // Load the PDF and edit annotations
        using (Document doc = new Document(inputPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the document to the facade (required before using page annotations)
            editor.BindPdf(doc);

            foreach (var def in batch)
            {
                // Ensure the requested page exists
                if (def.PageNumber < 1 || def.PageNumber > doc.Pages.Count)
                    continue; // skip invalid page numbers

                // Create a TextAnnotation (you can switch to other annotation types)
                TextAnnotation txtAnn = new TextAnnotation(doc.Pages[def.PageNumber], def.Rect)
                {
                    Title    = def.Title,
                    Contents = def.Contents,
                    Color    = def.Color
                };

                // Add the annotation to the page's collection
                doc.Pages[def.PageNumber].Annotations.Add(txtAnn);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}