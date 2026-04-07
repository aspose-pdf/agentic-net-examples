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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 5 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            Page page5 = doc.Pages[5];

            // LINQ query to retrieve the first RichMediaAnnotation on page 5
            RichMediaAnnotation richMedia = page5.Annotations
                .OfType<RichMediaAnnotation>()
                .FirstOrDefault();

            if (richMedia == null)
            {
                Console.WriteLine("No RichMediaAnnotation found on page 5.");
                return;
            }

            // Display selected properties of the annotation
            Console.WriteLine("RichMediaAnnotation found on page 5:");
            Console.WriteLine($"Name      : {richMedia.Name}");
            Console.WriteLine($"Contents  : {richMedia.Contents}");
            Console.WriteLine($"Type      : {richMedia.Type}");
            Console.WriteLine($"ActivateOn: {richMedia.ActivateOn}");
            Console.WriteLine($"Modified  : {richMedia.Modified}");
            Console.WriteLine($"Rect      : {richMedia.Rect}");
        }
    }
}