using System;
using System.IO;
using System.Drawing; // Required for DefaultAppearance constructor (System.Drawing.Color)
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance for the text (font, size, color)
            // Note: constructor requires System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, appearance);
            ft.Contents = "Overlay note with transparent background";
            // Transparent background color
            ft.Color = Aspose.Pdf.Color.Transparent;
            // Set opacity (0 = fully transparent, 1 = opaque)
            ft.Opacity = 0.2;
            // Optional: no border – set after the annotation is fully instantiated
            ft.Border = new Border(ft) { Width = 0 };

            // Add the annotation to the page
            page.Annotations.Add(ft);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
