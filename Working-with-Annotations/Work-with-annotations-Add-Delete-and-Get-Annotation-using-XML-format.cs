using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationXmlDemo
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string xfdfFile   = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a TextAnnotation (sticky note) to page 1
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing (global rule)

            // Fully qualified Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            TextAnnotation txtAnnot = new TextAnnotation(page, rect)
            {
                Title    = "Demo Note",
                Contents = "This is a sample annotation added via code.",
                Open     = true,
                Icon     = TextIcon.Note,
                Color    = Aspose.Pdf.Color.Yellow // cross‑platform Color (do‑not use System.Drawing)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(txtAnnot);

            // -------------------------------------------------
            // 2. Export current annotations to XFDF (XML format)
            // -------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfFile);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfFile}");

            // -------------------------------------------------
            // 3. Delete the annotation we just added
            // -------------------------------------------------
            // Annotations can be deleted by index or by reference.
            // Here we delete the first annotation on the page.
            if (page.Annotations.Count > 0)
            {
                page.Annotations.Delete(0);
                Console.WriteLine("Annotation deleted from the document.");
            }

            // Save the PDF after deletion
            doc.Save(outputPdf);
            Console.WriteLine($"PDF saved after deletion: {outputPdf}");

            // -------------------------------------------------
            // 4. Import annotations back from the XFDF file
            // -------------------------------------------------
            doc.ImportAnnotationsFromXfdf(xfdfFile);
            Console.WriteLine("Annotations imported back from XFDF.");

            // Save the final PDF with restored annotations
            string finalPdf = "final_output.pdf";
            doc.Save(finalPdf);
            Console.WriteLine($"Final PDF with restored annotations saved: {finalPdf}");

            // -------------------------------------------------
            // 5. Retrieve (Get) an annotation by name (if any)
            // -------------------------------------------------
            // Optionally, demonstrate fetching an annotation by its Name property.
            // First, ensure the annotation has a unique name.
            txtAnnot.Name = "DemoAnnotation01";

            // After import, locate the annotation by name.
            Annotation found = null;
            foreach (Annotation ann in page.Annotations)
            {
                if (ann.Name == "DemoAnnotation01")
                {
                    found = ann;
                    break;
                }
            }

            if (found != null)
            {
                Console.WriteLine($"Found annotation: Title='{((TextAnnotation)found).Title}', Contents='{found.Contents}'");
            }
            else
            {
                Console.WriteLine("Annotation with the specified name was not found.");
            }
        }
    }
}