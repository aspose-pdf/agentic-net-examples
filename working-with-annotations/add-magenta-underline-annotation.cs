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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the underline will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create an underline annotation
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect);

            // Set the annotation color to magenta (use Aspose.Pdf.Color, not System.Drawing)
            underline.Color = Aspose.Pdf.Color.FromRgb(255, 0, 255); // magenta

            // Set the line thickness to two points via Border (requires parent annotation)
            underline.Border = new Border(underline) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(underline);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
    }
}
