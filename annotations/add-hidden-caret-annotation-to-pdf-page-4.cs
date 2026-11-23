using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 4 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Retrieve page 4
            Page page = doc.Pages[4];

            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a CaretAnnotation with Subject and Hidden flag
            CaretAnnotation caret = new CaretAnnotation(page, rect)
            {
                Subject = "Comment",
                Flags   = AnnotationFlags.Hidden
            };

            // Add the annotation to the page
            page.Annotations.Add(caret);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Caret annotation added and saved to '{outputPath}'.");
    }
}