using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page5_cleared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document does not contain page 5.");
                return;
            }

            // Get page 5 (Aspose.Pdf pages are 1‑based)
            Page page5 = doc.Pages[5];

            // Collect widget annotations (form fields) that belong to page 5
            List<Annotation> widgetsOnPage5 = new List<Annotation>();
            foreach (Annotation annot in page5.Annotations)
            {
                if (annot is WidgetAnnotation)
                {
                    widgetsOnPage5.Add(annot);
                }
            }

            // Remove the collected widget annotations from the page (and thus from the form)
            foreach (Annotation widget in widgetsOnPage5)
            {
                page5.Annotations.Delete(widget);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 5 cleared and saved to '{outputPath}'.");
    }
}
