using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define a batch of annotation specifications
        var annotationDefs = new[]
        {
            new
            {
                Page = 1,
                Rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550),
                Contents = "First batch note",
                Color = Aspose.Pdf.Color.Yellow
            },
            new
            {
                Page = 2,
                Rect = new Aspose.Pdf.Rectangle(150, 400, 250, 450),
                Contents = "Second batch note",
                Color = Aspose.Pdf.Color.LightGreen
            },
            new
            {
                Page = 1,
                Rect = new Aspose.Pdf.Rectangle(300, 600, 400, 650),
                Contents = "Third batch note",
                Color = Aspose.Pdf.Color.Pink
            }
        };

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Use PdfAnnotationEditor (facade) to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Loop over the definitions and add each annotation
                foreach (var def in annotationDefs)
                {
                    // Validate page number (Aspose.Pdf uses 1‑based indexing)
                    if (def.Page < 1 || def.Page > doc.Pages.Count)
                        continue;

                    // Create a TextAnnotation for the current definition
                    TextAnnotation txtAnn = new TextAnnotation(doc.Pages[def.Page], def.Rect)
                    {
                        Contents = def.Contents,
                        Color = def.Color,
                        Title = "BatchNote",
                        Open = true,
                        Icon = TextIcon.Note
                    };

                    // Add the annotation to the page's collection
                    doc.Pages[def.Page].Annotations.Add(txtAnn);
                }

                // Save the modified document
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Batch annotations added and saved to '{outputPath}'.");
    }
}