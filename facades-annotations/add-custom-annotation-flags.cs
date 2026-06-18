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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor and bind it to the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Create a concrete annotation (TextAnnotation) with a page‑rectangle constructor
                // The rectangle can be zero‑size because we only want to modify flags of existing annotations
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                TextAnnotation customAnnot = new TextAnnotation(firstPage, rect)
                {
                    // Combine desired flags using the AnnotationFlags enum
                    Flags    = AnnotationFlags.Print | AnnotationFlags.Locked,
                    Title    = "Custom Flags",
                    Contents = "This annotation has custom flags applied.",
                    Color    = Aspose.Pdf.Color.Yellow
                };

                // Apply the annotation modifications to all pages (1‑based indexing)
                int startPage = 1;
                int endPage   = doc.Pages.Count;
                editor.ModifyAnnotations(startPage, endPage, customAnnot);

                // Save the modified PDF using the editor's Save method
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
