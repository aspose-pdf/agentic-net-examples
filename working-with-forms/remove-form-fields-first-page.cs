using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "template_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            const int firstPageNumber = 1;

            // Get the first page
            Page firstPage = doc.Pages[firstPageNumber];

            // Collect widget annotations that belong to form fields on the first page
            var widgetsOnFirstPage = new List<WidgetAnnotation>();
            foreach (Annotation annot in firstPage.Annotations)
            {
                if (annot is WidgetAnnotation widget)
                {
                    widgetsOnFirstPage.Add(widget);
                }
            }

            // Delete the corresponding form fields and their annotations
            foreach (WidgetAnnotation widget in widgetsOnFirstPage)
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field before deletion
                Field? field = doc.Form[widget.Name] as Field;
                if (field != null)
                {
                    doc.Form.Delete(field);
                }

                // Remove the widget annotation from the page
                firstPage.Annotations.Delete(widget);
            }

            // Flatten the page to ensure no leftover form artifacts remain
            firstPage.Flatten();

            // Save the modified PDF (creates a clean template page)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Template page saved to '{outputPath}'.");
    }
}
