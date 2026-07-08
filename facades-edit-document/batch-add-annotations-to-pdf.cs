using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Definition of an annotation to be added
    private class AnnotationDefinition
    {
        // Use required auto‑properties (C# 11) to satisfy non‑nullable analysis
        public required string Type { get; init; }
        public required int PageNumber { get; init; }
        public required Aspose.Pdf.Rectangle Rect { get; init; }
        public required string Content { get; init; }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the facade that works with annotations
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Prepare a batch of annotation definitions
        AnnotationDefinition[] annotations = new AnnotationDefinition[]
        {
            new AnnotationDefinition
            {
                Type = "Text",
                PageNumber = 1,
                Rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750),
                Content = "First batch text annotation"
            },
            new AnnotationDefinition
            {
                Type = "Link",
                PageNumber = 1,
                Rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650),
                Content = "https://www.example.com"
            },
            new AnnotationDefinition
            {
                Type = "Text",
                PageNumber = 2,
                Rect = new Aspose.Pdf.Rectangle(50, 500, 250, 550),
                Content = "Second page note"
            }
        };

        // Loop over the definitions and add the corresponding annotations
        foreach (var def in annotations)
        {
            // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle for the facade API
            var sysRect = new System.Drawing.Rectangle(
                (int)def.Rect.LLX,
                (int)def.Rect.LLY,
                (int)(def.Rect.URX - def.Rect.LLX),
                (int)(def.Rect.URY - def.Rect.LLY));

            switch (def.Type)
            {
                case "Text":
                    // Create a free‑text annotation. The overload requires author, visibility flag and subject.
                    // Provide sensible defaults: author = "Aspose", isVisible = true, subject = "Annotation".
                    editor.CreateText(sysRect, def.Content, "Aspose", true, "Annotation", def.PageNumber);
                    break;

                case "Link":
                    // Create a web link annotation.
                    editor.CreateWebLink(sysRect, def.Content, def.PageNumber);
                    break;

                default:
                    Console.WriteLine($"Unsupported annotation type: {def.Type}");
                    break;
            }
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Annotations added and saved to '{outputPdf}'.");
    }
}
