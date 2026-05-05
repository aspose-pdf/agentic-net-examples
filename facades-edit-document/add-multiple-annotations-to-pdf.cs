using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple definition for an annotation to be added
    class AnnotationDef
    {
        public string Type;               // "Text" or "Highlight"
        public int PageNumber;            // 1‑based page index
        public double Llx, Lly, Urx, Ury; // rectangle coordinates
        public string Contents;           // text for TextAnnotation or comment for Highlight
        public string Title;              // optional title for TextAnnotation
        public Color Color;               // annotation color
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define a batch of annotations
        var annotations = new List<AnnotationDef>
        {
            new AnnotationDef
            {
                Type = "Text",
                PageNumber = 1,
                Llx = 100, Lly = 500, Urx = 200, Ury = 550,
                Contents = "First note",
                Title = "Note 1",
                Color = Aspose.Pdf.Color.Yellow
            },
            new AnnotationDef
            {
                Type = "Highlight",
                PageNumber = 1,
                Llx = 150, Lly = 400, Urx = 350, Ury = 420,
                Contents = "Important text",
                Color = Aspose.Pdf.Color.LightGreen
            },
            new AnnotationDef
            {
                Type = "Text",
                PageNumber = 2,
                Llx = 50, Lly = 600, Urx = 250, Ury = 650,
                Contents = "Second note",
                Title = "Note 2",
                Color = Aspose.Pdf.Color.Pink
            }
        };

        // Load the PDF, add annotations in a batch, and save
        using (Document doc = new Document(inputPdf))
        {
            foreach (var def in annotations)
            {
                // Ensure the page exists
                if (def.PageNumber < 1 || def.PageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {def.PageNumber} for annotation.");
                    continue;
                }

                Page page = doc.Pages[def.PageNumber];
                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(def.Llx, def.Lly, def.Urx, def.Ury);

                Annotation annotation = null;

                if (def.Type.Equals("Text", StringComparison.OrdinalIgnoreCase))
                {
                    // Create a TextAnnotation (a sticky note)
                    var textAnn = new TextAnnotation(page, rect)
                    {
                        Title    = def.Title ?? string.Empty,
                        Contents = def.Contents ?? string.Empty,
                        Color    = def.Color
                    };
                    annotation = textAnn;
                }
                else if (def.Type.Equals("Highlight", StringComparison.OrdinalIgnoreCase))
                {
                    // Create a HighlightAnnotation
                    var highlightAnn = new HighlightAnnotation(page, rect)
                    {
                        Contents = def.Contents ?? string.Empty,
                        Color    = def.Color
                    };
                    annotation = highlightAnn;
                }
                else
                {
                    Console.Error.WriteLine($"Unsupported annotation type: {def.Type}");
                    continue;
                }

                // Add the annotation to the page's collection
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPdf}'.");
    }
}