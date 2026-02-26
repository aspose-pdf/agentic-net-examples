using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string addedPdf  = "added_annotation.pdf";
        const string cleanedPdf = "deleted_annotation.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -------------------------------------------------
        // 1. Load the document and add a TextAnnotation
        // -------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Choose the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation with TeX content
            TextAnnotation texAnn = new TextAnnotation(page, rect)
            {
                Title    = "Author",
                Contents = "$E=mc^2$",          // TeX formatted string
                Open     = true,                // Show popup open by default
                Icon     = TextIcon.Note         // Standard sticky‑note icon
            };

            // Add the annotation to the page
            page.Annotations.Add(texAnn);

            // Save the document with the new annotation
            doc.Save(addedPdf);
        }

        Console.WriteLine($"Annotation added and saved to '{addedPdf}'.");

        // -------------------------------------------------
        // 2. Load the document, retrieve the annotation text
        // -------------------------------------------------
        using (Document doc = new Document(addedPdf))
        {
            Page page = doc.Pages[1];
            TextAnnotation foundAnn = null;

            // Iterate through all annotations on the page
            foreach (Annotation ann in page.Annotations)
            {
                if (ann is TextAnnotation txtAnn)
                {
                    foundAnn = txtAnn;
                    break; // Assuming only one for this demo
                }
            }

            if (foundAnn != null)
            {
                Console.WriteLine($"Retrieved annotation contents: {foundAnn.Contents}");
            }
            else
            {
                Console.WriteLine("No TextAnnotation found.");
            }

            // -------------------------------------------------
            // 3. Delete the annotation and save the result
            // -------------------------------------------------
            if (foundAnn != null)
            {
                // Remove the annotation from the collection
                page.Annotations.Remove(foundAnn);
                doc.Save(cleanedPdf);
                Console.WriteLine($"Annotation deleted and saved to '{cleanedPdf}'.");
            }
        }
    }
}