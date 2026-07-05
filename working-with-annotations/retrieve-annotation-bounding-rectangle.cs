using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string annotationName = "MyAnnotation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Find the annotation by its name; returns null if not found
            Annotation ann = page.Annotations.FindByName(annotationName);
            if (ann == null)
            {
                Console.WriteLine($"Annotation '{annotationName}' not found on page 1.");
                return;
            }

            // Retrieve the bounding rectangle, taking page rotation into account
            Aspose.Pdf.Rectangle rect = ann.GetRectangle(true);

            // Log rectangle dimensions
            Console.WriteLine($"Annotation '{annotationName}' rectangle:");
            Console.WriteLine($"  Lower‑left X : {rect.LLX}");
            Console.WriteLine($"  Lower‑left Y : {rect.LLY}");
            Console.WriteLine($"  Upper‑right X: {rect.URX}");
            Console.WriteLine($"  Upper‑right Y: {rect.URY}");
            Console.WriteLine($"  Width  : {rect.URX - rect.LLX}");
            Console.WriteLine($"  Height : {rect.URY - rect.LLY}");
        }
    }
}