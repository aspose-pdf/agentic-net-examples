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
            // Ensure the document has at least 5 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            // Get page 5
            Page pageFive = doc.Pages[5];

            // LINQ query to retrieve the first RichMediaAnnotation on page 5
            RichMediaAnnotation richMedia = pageFive.Annotations
                                                    .OfType<RichMediaAnnotation>()
                                                    .FirstOrDefault();

            if (richMedia == null)
            {
                Console.WriteLine("No RichMediaAnnotation found on page 5.");
                return;
            }

            // Display selected properties of the RichMediaAnnotation
            Console.WriteLine("RichMediaAnnotation found on page 5:");
            Console.WriteLine($"  Name               : {richMedia.Name}");
            Console.WriteLine($"  Contents (text)    : {richMedia.Contents}");
            Console.WriteLine($"  Type (Audio/Video) : {richMedia.Type}");
            Console.WriteLine($"  ActivateOn         : {richMedia.ActivateOn}");
            Console.WriteLine($"  Modified           : {richMedia.Modified}");
            Console.WriteLine($"  Rect (LLX,LLY,URX,URY): {richMedia.Rect}");
            Console.WriteLine($"  Width              : {richMedia.Width}");
            Console.WriteLine($"  Height             : {richMedia.Height}");
            Console.WriteLine($"  Color              : {richMedia.Color}");
        }
    }
}