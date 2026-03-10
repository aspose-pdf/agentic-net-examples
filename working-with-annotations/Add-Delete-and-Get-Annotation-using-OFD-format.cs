using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputOfdPath  = "input.ofd";      // OFD input file
        const string outputPdfPath = "output.pdf";     // PDF output (OFD cannot be saved)

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"File not found: {inputOfdPath}");
            return;
        }

        // Load OFD using the dedicated load options (OFD is input‑only)
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // -------------------------------------------------
            // ADD AN ANNOTATION
            // -------------------------------------------------
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (cross‑platform, no System.Drawing usage)
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is a sample annotation added to an OFD document.",
                Color    = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(txtAnn);

            // -------------------------------------------------
            // GET (RETRIEVE) THE ANNOTATION
            // -------------------------------------------------
            // Retrieve by name (if a name was set) or by index.
            // Here we use the index; annotation collections are 1‑based.
            Annotation retrieved = page.Annotations[1];
            Console.WriteLine($"Retrieved Annotation - Title: {(retrieved as TextAnnotation)?.Title}");
            Console.WriteLine($"Contents: {retrieved.Contents}");

            // -------------------------------------------------
            // DELETE THE ANNOTATION
            // -------------------------------------------------
            // Delete by index (1‑based). This removes the annotation we just added.
            page.Annotations.Delete(1);

            // -------------------------------------------------
            // SAVE RESULT
            // -------------------------------------------------
            // OFD cannot be saved; we export to PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPdfPath}'.");
    }
}