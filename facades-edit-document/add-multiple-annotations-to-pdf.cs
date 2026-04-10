using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    // Simple DTO to describe an annotation to be added
    private class AnnotationDefinition
    {
        public string Type;          // "Text", "Highlight", "Link"
        public Aspose.Pdf.Rectangle Rect;
        public string Content;      // Text for Text/Highlight, URL for Link
        public string Title;        // Optional title for Text annotation
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_annotated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define a batch of annotations
        var annotations = new List<AnnotationDefinition>
        {
            new AnnotationDefinition
            {
                Type = "Text",
                Rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750),
                Title = "Note 1",
                Content = "This is a text annotation."
            },
            new AnnotationDefinition
            {
                Type = "Highlight",
                Rect = new Aspose.Pdf.Rectangle(50, 600, 400, 620),
                Content = "Highlighted text."
            },
            new AnnotationDefinition
            {
                Type = "Link",
                Rect = new Aspose.Pdf.Rectangle(200, 500, 350, 530),
                Content = "https://www.example.com"
            }
        };

        // Use PdfAnnotationEditor to bind, modify, and save the PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdf);

            // Access the underlying Document to add annotations
            Document doc = editor.Document;

            // For simplicity, add all annotations to the first page
            Page page = doc.Pages[1]; // 1‑based indexing

            foreach (var def in annotations)
            {
                Annotation ann = null;

                switch (def.Type)
                {
                    case "Text":
                        var textAnn = new TextAnnotation(page, def.Rect)
                        {
                            Title    = def.Title ?? string.Empty,
                            Contents = def.Content ?? string.Empty,
                            Color    = Aspose.Pdf.Color.Yellow
                        };
                        ann = textAnn;
                        break;

                    case "Highlight":
                        var highlightAnn = new HighlightAnnotation(page, def.Rect)
                        {
                            Color = Aspose.Pdf.Color.Yellow
                        };
                        // Optionally set the highlighted text as contents
                        highlightAnn.Contents = def.Content ?? string.Empty;
                        ann = highlightAnn;
                        break;

                    case "Link":
                        var linkAnn = new LinkAnnotation(page, def.Rect);
                        // Use GoToURIAction for external URLs
                        linkAnn.Action = new GoToURIAction(def.Content);
                        linkAnn.Color = Aspose.Pdf.Color.Blue;
                        ann = linkAnn;
                        break;

                    default:
                        Console.WriteLine($"Unsupported annotation type: {def.Type}");
                        continue;
                }

                // Add the created annotation to the page
                page.Annotations.Add(ann);
            }

            // Save the modified document
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPdf}'.");
    }
}