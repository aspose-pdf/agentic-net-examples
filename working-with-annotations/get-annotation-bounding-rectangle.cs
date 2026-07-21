using System;
using System.IO;
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Target the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Verify that the page contains at least one annotation
            if (page.Annotations.Count == 0)
            {
                Console.WriteLine("No annotations found on page 1.");
                return;
            }

            // Retrieve the first annotation (annotation collections are also 1‑based)
            Annotation annotation = page.Annotations[1];

            // Obtain the bounding rectangle, taking page rotation into account
            Aspose.Pdf.Rectangle rect = annotation.GetRectangle(true);

            // Calculate width and height from rectangle coordinates
            double width = rect.URX - rect.LLX;
            double height = rect.URY - rect.LLY;

            // Log the details
            Console.WriteLine($"Annotation Type: {annotation.AnnotationType}");
            Console.WriteLine($"Rectangle (LLX, LLY, URX, URY): {rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}");
            Console.WriteLine($"Width: {width}");
            Console.WriteLine($"Height: {height}");
        }
    }
}