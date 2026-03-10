using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string addedPath  = "added_annotation.pdf";
        const string finalPath  = "deleted_annotation.pdf";

        // -------------------------------------------------
        // 1. Load the PDF and add a TextAnnotation (sticky note)
        // -------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the TextAnnotation on the page
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note Title",
                Contents = "This is a sample annotation added via Aspose.Pdf.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,               // Show the popup open by default
                Icon     = TextIcon.Note        // Use the standard note icon
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(txtAnn);

            // Save the document with the new annotation
            doc.Save(addedPath);
        }

        // -------------------------------------------------
        // 2. Load the PDF with the added annotation and retrieve it
        // -------------------------------------------------
        using (Document doc = new Document(addedPath))
        {
            Page page = doc.Pages[1];

            // Annotations collection uses 1‑based indexing
            if (page.Annotations.Count > 0)
            {
                // Get the first annotation (index 1)
                Annotation ann = page.Annotations[1];

                // Cast to TextAnnotation to access specific properties
                if (ann is TextAnnotation txtAnn)
                {
                    Console.WriteLine($"Annotation Title   : {txtAnn.Title}");
                    Console.WriteLine($"Annotation Contents: {txtAnn.Contents}");
                    Console.WriteLine($"Annotation Color   : {txtAnn.Color}");
                }
            }

            // -------------------------------------------------
            // 3. Delete the annotation and save the result
            // -------------------------------------------------
            // Delete by index (1‑based)
            page.Annotations.Delete(1);

            // Alternatively, you could delete by passing the annotation instance:
            // page.Annotations.Delete(ann);

            doc.Save(finalPath);
        }

        Console.WriteLine("Annotation added, retrieved, and deleted successfully.");
    }
}