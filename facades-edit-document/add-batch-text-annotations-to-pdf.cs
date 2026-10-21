using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

namespace AsposePdfApi
{
    // Simple DTO that describes an annotation to be added.
    // All properties are required and set via the constructor to avoid nullable warnings.
    public class AnnotationDefinition
    {
        public Aspose.Pdf.Rectangle Bounds { get; }
        public string Text { get; }

        public AnnotationDefinition(Aspose.Pdf.Rectangle bounds, string text)
        {
            Bounds = bounds ?? throw new ArgumentNullException(nameof(bounds));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }

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

            // Define a batch of annotations.
            var annotations = new List<AnnotationDefinition>
            {
                new AnnotationDefinition(new Aspose.Pdf.Rectangle(100, 700, 300, 750), "First note"),
                new AnnotationDefinition(new Aspose.Pdf.Rectangle(100, 600, 300, 650), "Second note"),
                new AnnotationDefinition(new Aspose.Pdf.Rectangle(100, 500, 300, 550), "Third note")
            };

            // Open the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // Instantiate a Facades editor – required by the task specification.
                // The editor works with the same Document instance.
                PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor(doc);

                // For this example we add all annotations to the first page.
                // Adjust the page index as needed (Aspose.Pdf uses 1‑based indexing).
                Page page = doc.Pages[1];

                foreach (AnnotationDefinition def in annotations)
                {
                    // Create a TextAnnotation using the rectangle bounds from the definition.
                    TextAnnotation txtAnn = new TextAnnotation(page, def.Bounds)
                    {
                        Title    = "Note",
                        Contents = def.Text,
                        Open     = true,
                        Icon     = TextIcon.Note
                    };

                    // Add the annotation to the page.
                    page.Annotations.Add(txtAnn);
                }

                // Save the modified document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
        }
    }
}