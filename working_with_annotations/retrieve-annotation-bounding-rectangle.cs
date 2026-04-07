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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Assume we are interested in the first page
            Page page = doc.Pages[1];

            // Ensure the page contains at least one annotation
            if (page.Annotations.Count == 0)
            {
                Console.WriteLine("No annotations found on the first page.");
                return;
            }

            // Retrieve the first annotation (annotation collections are 1‑based)
            Annotation annotation = page.Annotations[1];

            // Get the bounding rectangle, taking page rotation into account
            Aspose.Pdf.Rectangle rect = annotation.GetRectangle(true);

            // Log rectangle dimensions
            Console.WriteLine("Annotation Bounding Rectangle:");
            Console.WriteLine($"  Lower‑Left X : {rect.LLX}");
            Console.WriteLine($"  Lower‑Left Y : {rect.LLY}");
            Console.WriteLine($"  Upper‑Right X: {rect.URX}");
            Console.WriteLine($"  Upper‑Right Y: {rect.URY}");
        }
    }
}