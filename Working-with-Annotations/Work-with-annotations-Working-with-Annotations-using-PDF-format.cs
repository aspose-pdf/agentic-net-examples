using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged; // for ITaggedContent if needed (not used here)

class AnnotationDemo
{
    static void Main()
    {
        const string inputPdf = "source.pdf";
        const string exportedXfdf = "annotations_exported.xfdf";
        const string importXfdf = "annotations_to_import.xfdf";
        const string outputPdf = "result.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // Export existing annotations to an XFDF file
            // -------------------------------------------------
            doc.ExportAnnotationsToXfdf(exportedXfdf);
            Console.WriteLine($"Exported annotations to '{exportedXfdf}'.");

            // -------------------------------------------------
            // Import annotations from another XFDF file (if it exists)
            // -------------------------------------------------
            if (File.Exists(importXfdf))
            {
                doc.ImportAnnotationsFromXfdf(importXfdf);
                Console.WriteLine($"Imported annotations from '{importXfdf}'.");
            }
            else
            {
                Console.WriteLine($"Import XFDF not found: {importXfdf} (skipping import).");
            }

            // -------------------------------------------------
            // Add a new TextAnnotation (sticky note) to the first page
            // -------------------------------------------------
            // Use fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation txtAnno = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Note",
                Contents = "Added via Aspose.Pdf API",
                Open = true,
                Icon = TextIcon.Note,
                Color = Aspose.Pdf.Color.Yellow
            };
            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(txtAnno);
            Console.WriteLine("Added a new TextAnnotation to page 1.");

            // -------------------------------------------------
            // Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
    }
}