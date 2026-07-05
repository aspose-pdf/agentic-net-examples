using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";
        const string annotationId = "myAnnotationId"; // the Name of the annotation to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            bool found = false;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Annotation collections are also 1‑based
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // The identifier of an annotation is stored in its Name property
                    if (ann.Name == annotationId)
                    {
                        // Example: move the annotation to a new rectangle (llx, lly, urx, ury)
                        // Adjust these values as needed for your layout
                        double llx = 100; // lower‑left X
                        double lly = 500; // lower‑left Y
                        double urx = 300; // upper‑right X
                        double ury = 600; // upper‑right Y

                        // Fully qualify the Rectangle type to avoid ambiguity
                        ann.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                        // If you need to consider page rotation when setting the rectangle,
                        // you can use the GetRectangle method with true and then assign it back.
                        // Example:
                        // Aspose.Pdf.Rectangle rotatedRect = ann.GetRectangle(true);
                        // // modify rotatedRect as needed
                        // ann.Rect = rotatedRect;

                        found = true;
                        Console.WriteLine($"Annotation '{annotationId}' on page {pageNum} updated.");
                        break; // exit inner loop once found
                    }
                }

                if (found) break; // exit outer loop once found
            }

            if (!found)
            {
                Console.WriteLine($"Annotation with Id '{annotationId}' not found.");
            }

            // Save the modified PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}