using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class UpdateAnnotationsExample
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "updated_annotations.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Iterate through all pages and their annotations
        foreach (Page page in pdfDocument.Pages)
        {
            if (page.Annotations == null) continue;

            foreach (Annotation annotation in page.Annotations)
            {
                // We are interested only in TextAnnotation (a markup annotation type)
                if (annotation is TextAnnotation textAnnot)
                {
                    // Update desired properties. Only the properties set here will be changed.
                    textAnnot.Title    = "Revised Title"; // MarkupAnnotation property
                    textAnnot.Contents = "The annotation content has been updated programmatically.";
                    textAnnot.Color    = Aspose.Pdf.Color.Green; // Border color is controlled by the annotation's own Color
                    textAnnot.Subject  = "Updated Subject";
                    textAnnot.Open     = true; // Expanded when opened
                    textAnnot.Modified = DateTime.Now;
                }
            }
        }

        // Save the modified PDF. The original structure (pages, objects) remains unchanged.
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Annotations updated and saved to '{outputPdf}'.");
    }
}
