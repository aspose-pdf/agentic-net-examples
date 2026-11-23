using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "DraftUnderlined.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the underline will appear
            // (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 500);

            // Create the underline annotation
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect)
            {
                Title = "Review",                     // Annotation title
                Color = Aspose.Pdf.Color.Blue        // Blue annotation color
            };

            // Attach the annotation to the page
            page.Annotations.Add(underline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underlined PDF saved to '{outputPath}'.");
    }
}