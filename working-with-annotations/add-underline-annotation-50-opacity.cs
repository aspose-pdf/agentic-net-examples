using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the target page (1‑based indexing)
            int targetPageNumber = 1;
            Page page = doc.Pages[targetPageNumber];

            // Define the rectangle where the underline will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the underline annotation and set its opacity to 50%
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect)
            {
                Opacity = 0.5 // 0 = fully transparent, 1 = fully opaque
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(underline);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation with 50% opacity saved to '{outputPath}'.");
    }
}