using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string psPath = "input.ps";          // source PostScript file
        const string outputPdf = "output.pdf";     // result PDF file (PS cannot be saved)

        if (!File.Exists(psPath))
        {
            Console.Error.WriteLine($"File not found: {psPath}");
            return;
        }

        // Load the PS file – PsLoadOptions is the only way to read PostScript
        using (Document doc = new Document(psPath, new PsLoadOptions()))
        {
            // Ensure at least one page was created after conversion
            if (doc.Pages.Count == 0)
            {
                Console.WriteLine("No pages were loaded from the PS file.");
                return;
            }

            // -------------------------------------------------
            // Add a TextAnnotation to the first page
            // -------------------------------------------------
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",                                   // title shown in popup
                Contents = "Sample annotation added programmatically.", // visible text
                Color    = Aspose.Pdf.Color.Yellow,                 // annotation border color
                Name     = "MyAnnotation"                           // unique name for lookup
            };
            page.Annotations.Add(txtAnn); // add to the page's annotation collection

            // -------------------------------------------------
            // Get (retrieve) the annotation by its name
            // -------------------------------------------------
            Annotation found = page.Annotations.FindByName("MyAnnotation");
            if (found != null)
            {
                // Title is defined on MarkupAnnotation, so cast safely
                var markup = found as MarkupAnnotation;
                Console.WriteLine($"Found annotation – Title: {markup?.Title}, Contents: {found.Contents}");
            }

            // -------------------------------------------------
            // Delete the annotation (by reference)
            // -------------------------------------------------
            if (found != null)
            {
                page.Annotations.Delete(found);
            }

            // Verify that the annotation was removed
            Annotation afterDelete = page.Annotations.FindByName("MyAnnotation");
            Console.WriteLine(afterDelete == null ? "Annotation successfully deleted." : "Deletion failed.");

            // -------------------------------------------------
            // Save the modified document.
            // PS cannot be used as an output format, so we save as PDF.
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}