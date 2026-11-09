using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for annotation related types

class Program
{
    // Simple DTO describing an annotation to be added
    class AnnotationDefinition
    {
        public int PageNumber { get; set; }          // 1‑based page index
        public double Llx { get; set; }              // lower‑left X
        public double Lly { get; set; }              // lower‑left Y
        public double Urx { get; set; }              // upper‑right X
        public double Ury { get; set; }              // upper‑right Y
        public string Title { get; set; } = string.Empty;    // annotation title (for text annotations)
        public string Contents { get; set; } = string.Empty; // annotation body
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "annotated_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Prepare a batch of annotation definitions
        List<AnnotationDefinition> batch = new List<AnnotationDefinition>
        {
            new AnnotationDefinition
            {
                PageNumber = 1,
                Llx = 100, Lly = 500, Urx = 300, Ury = 550,
                Title = "Note 1",
                Contents = "First annotation added in batch."
            },
            new AnnotationDefinition
            {
                PageNumber = 2,
                Llx = 150, Lly = 400, Urx = 350, Ury = 450,
                Title = "Note 2",
                Contents = "Second annotation on page 2."
            },
            new AnnotationDefinition
            {
                PageNumber = 1,
                Llx = 50, Lly = 700, Urx = 250, Ury = 750,
                Title = "Reminder",
                Contents = "Check this section later."
            }
        };

        // Load the source PDF document
        Document doc = new Document(inputPdf);

        // Loop over the batch and add each annotation
        foreach (AnnotationDefinition def in batch)
        {
            // Ensure the page index is within the document range
            if (def.PageNumber < 1 || def.PageNumber > doc.Pages.Count)
                continue; // or handle the error as needed

            Page page = doc.Pages[def.PageNumber];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(def.Llx, def.Lly, def.Urx, def.Ury);

            // Create a TextAnnotation and set its properties
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title = def.Title,
                Contents = def.Contents
            };

            // Add the annotation to the page
            page.Annotations.Add(txtAnn);
        }

        // Save the modified PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Annotations added successfully. Output saved to '{outputPdf}'.");
    }
}
