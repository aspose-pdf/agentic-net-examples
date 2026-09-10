using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;          // for DefaultAppearance
using System.Drawing;          // System.Drawing.Color required by DefaultAppearance

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            // Get page five (1‑based indexing)
            Page pageFive = doc.Pages[5];

            // Define the rectangle for the free‑text annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance (font name, size, color)
            // Note: the constructor expects System.Drawing.Color for the third argument
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation on page five
            FreeTextAnnotation freeText = new FreeTextAnnotation(pageFive, rect, appearance)
            {
                Contents = "This annotation is hidden but its data is retained.",
                // Optional: set a background color or other visual properties if needed
                Color = Aspose.Pdf.Color.LightGray
            };

            // Hide the annotation while keeping its data using the annotation flags
            freeText.Flags = AnnotationFlags.Hidden;

            // Add the annotation to the page
            pageFive.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation hidden on page 5 and saved to '{outputPath}'.");
    }
}
