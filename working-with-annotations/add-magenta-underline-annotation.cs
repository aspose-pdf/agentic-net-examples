using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "underline_magenta.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (create‑load‑save lifecycle)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the underline will appear
            // (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the underline annotation
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect);

            // Set the annotation color to magenta
            underline.Color = Aspose.Pdf.Color.Magenta;

            // Set the border thickness to 2 points.
            // Border requires the parent annotation in its constructor.
            underline.Border = new Border(underline) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(underline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation saved to '{outputPath}'.");
    }
}