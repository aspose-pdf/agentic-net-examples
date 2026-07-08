using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageToClear = 5; // page numbers are 1‑based in Aspose.Pdf

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageToClear > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages. Cannot clear page {pageToClear}.");
                return;
            }

            // Get the target page
            Page targetPage = doc.Pages[pageToClear];

            // Form fields are stored as WidgetAnnotation objects on the page.
            // Collect all widget annotations on the page.
            var widgetsOnPage = targetPage.Annotations
                                          .OfType<WidgetAnnotation>()
                                          .ToList(); // materialize to avoid modifying collection while iterating

            // Remove each widget annotation and its associated form field.
            foreach (WidgetAnnotation widget in widgetsOnPage)
            {
                // In Aspose.Pdf the widget itself *is* the form field (it derives from Field).
                // Cast to Field and delete it from the document's Form collection.
                if (widget is Field field)
                {
                    doc.Form.Delete(field);
                }

                // Delete the annotation from the page.
                targetPage.Annotations.Delete(widget);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToClear} cleared of form fields and saved to '{outputPath}'.");
    }
}
