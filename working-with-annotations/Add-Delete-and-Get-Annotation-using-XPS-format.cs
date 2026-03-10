using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputXps  = "input.xps";
        const string outputXps = "output.xps";

        if (!File.Exists(inputXps))
        {
            Console.Error.WriteLine($"File not found: {inputXps}");
            return;
        }

        // Load XPS document (XPS is an input‑only format, loaded as a PDF document)
        using (Document doc = new Document(inputXps, new XpsLoadOptions()))
        {
            // -------------------------------------------------
            // 1. Add a Text annotation to the first page
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample Note",
                Contents = "This is a newly added annotation.",
                Color    = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page
            page.Annotations.Add(txtAnn);

            // -------------------------------------------------
            // 2. Delete an annotation by its name using PdfAnnotationEditor
            // -------------------------------------------------
            // The annotation created above does not have a Name property set.
            // For demonstration, set a name and then delete it.
            txtAnn.Name = "NoteToDelete";

            // Use the facade to delete the annotation
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.DeleteAnnotation("NoteToDelete");
            editor.Save(outputXps); // Save after deletion (will be overwritten later)

            // -------------------------------------------------
            // 3. Get (read) an existing annotation
            // -------------------------------------------------
            // Re‑add the annotation so we can read it back
            page.Annotations.Add(txtAnn);

            // Retrieve the first annotation on the page
            Annotation ann = page.Annotations[1]; // 1‑based indexing

            // Title is available only on markup annotations
            string title = ann is MarkupAnnotation markup ? markup.Title : "(no title)";
            Console.WriteLine($"Annotation Title: {title}");
            Console.WriteLine($"Annotation Contents: {ann.Contents}");

            // -------------------------------------------------
            // 4. Save the modified document back to XPS format
            // -------------------------------------------------
            // Save using SaveFormat.Xps (XPS is an output format via SaveFormat)
            doc.Save(outputXps, SaveFormat.Xps);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputXps}'.");
    }
}