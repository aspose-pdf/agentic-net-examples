using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Operators; // for Border class

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

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the underline annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the underline annotation
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect)
            {
                // Set the annotation color to magenta
                Color = Aspose.Pdf.Color.Magenta
            };

            // Set the line thickness via the Border property.
            // Border requires the parent annotation in its constructor.
            underline.Border = new Border(underline) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(underline);

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation saved to '{outputPath}'.");
    }
}