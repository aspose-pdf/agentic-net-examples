using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "underlined.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (first page in this example)
            Page page = doc.Pages[1];

            // Create an underline annotation
            // The rectangle defines the location of the underline on the page
            // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect);

            // Set the annotation color to magenta using Aspose.Pdf.Color (cross‑platform)
            underline.Color = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 1.0); // magenta

            // Set the line thickness to two points via the Border.Width property
            underline.Border = new Border(underline) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(underline);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation saved to '{outputPath}'.");
    }
}
