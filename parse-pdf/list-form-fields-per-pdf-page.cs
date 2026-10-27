using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];

                // Form fields are represented as WidgetAnnotation objects on a page.
                var widgetAnnotations = page.Annotations
                                            .OfType<WidgetAnnotation>()
                                            .ToList();

                if (widgetAnnotations.Any())
                {
                    Console.WriteLine($"Page {pageNumber} contains {widgetAnnotations.Count} form field(s):");
                    foreach (WidgetAnnotation widget in widgetAnnotations)
                    {
                        // The field name is stored in the Name property of the widget.
                        string name = widget.Name;
                        string typeName = widget.GetType().Name;
                        Console.WriteLine($"  - {name} ({typeName})");
                    }
                }
                else
                {
                    // No form fields on this page
                    Console.WriteLine($"Page {pageNumber} contains no form fields.");
                }
            }
        }
    }
}
