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

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 5 pages (page indexing is 1‑based)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            // Get page 5
            Page pageFive = doc.Pages[5];

            // LINQ query to find the first RichMediaAnnotation on page 5
            RichMediaAnnotation richMedia = pageFive.Annotations
                                                   .OfType<RichMediaAnnotation>()
                                                   .FirstOrDefault();

            if (richMedia == null)
            {
                Console.WriteLine("No RichMediaAnnotation found on page 5.");
                return;
            }

            // Display selected properties
            Console.WriteLine("RichMediaAnnotation found:");
            Console.WriteLine($"  Name          : {richMedia.Name}");
            Console.WriteLine($"  Contents      : {richMedia.Contents}");
            Console.WriteLine($"  Type          : {richMedia.Type}");
            Console.WriteLine($"  ActivateOn    : {richMedia.ActivateOn}");
            Console.WriteLine($"  Modified      : {richMedia.Modified}");
            Aspose.Pdf.Rectangle rect = richMedia.Rect;
            Console.WriteLine($"  Rectangle     : LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}");
        }
    }
}