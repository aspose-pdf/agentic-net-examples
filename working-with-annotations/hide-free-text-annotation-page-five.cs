using System;
using System.IO;
using System.Drawing;                     // needed for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;                    // for DefaultAppearance

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

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance (font name, size, System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation on page five
            FreeTextAnnotation freeText = new FreeTextAnnotation(pageFive, rect, appearance)
            {
                Contents = "This annotation is hidden but its data is retained.",
                // Optional: set a background color or other visual properties if needed
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the annotation to the page
            pageFive.Annotations.Add(freeText);

            // Hide the annotation while keeping its data
            // The Hidden flag makes the annotation invisible in the viewer
            freeText.Flags = AnnotationFlags.Hidden;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden free‑text annotation on page 5: {outputPath}");
    }
}