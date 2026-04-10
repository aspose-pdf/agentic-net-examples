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
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Prepare a concrete annotation (TextAnnotation) using the (Page, Rectangle) constructor
            // This is required because TextAnnotation does not expose a public parameter‑less constructor.
            Page firstPage = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation customAnnot = new TextAnnotation(firstPage, rect)
            {
                Title = "Custom Flag",
                Contents = "Annotation with custom flags",
                Color = Aspose.Pdf.Color.Yellow,
                // Combine desired flags (e.g., Print and Locked)
                Flags = AnnotationFlags.Print | AnnotationFlags.Locked
            };

            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Modify annotations on the desired page range (1‑based indexing)
                int startPage = 1;
                int endPage = doc.Pages.Count;
                editor.ModifyAnnotations(startPage, endPage, customAnnot);

                // Save the modified PDF (lifecycle rule: use provided Save method)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
