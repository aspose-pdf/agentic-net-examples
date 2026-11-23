using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "caret_rects.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Verify that page 3 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF has fewer than 3 pages.");
                return;
            }

            // Retrieve page 3
            Page page = doc.Pages[3];

            // Open a text writer for the output file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Filter only CaretAnnotations
                    if (annot is CaretAnnotation caret)
                    {
                        // Use the correct property 'Rect' for annotation bounds
                        Aspose.Pdf.Rectangle rect = caret.Rect;
                        // Write rectangle coordinates: llx, lly, urx, ury
                        writer.WriteLine($"{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}");
                    }
                }
            }
        }

        Console.WriteLine($"Caret annotation rectangles have been written to '{outputPath}'.");
    }
}
