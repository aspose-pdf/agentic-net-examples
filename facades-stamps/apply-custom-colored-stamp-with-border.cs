using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;   // for Rectangle
using Aspose.Pdf;           // for Color (fully qualified when needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the stamp rectangle (coordinates are in points; origin is bottom‑left)
            // Here we place a 150x50 stamp near the top‑left corner of each page
            Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(50, 750, 200, 800);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a rubber‑stamp annotation on the current page
                StampAnnotation stamp = new StampAnnotation(page, stampRect)
                {
                    // Text displayed inside the stamp
                    Contents = "IMPORTANT",

                    // Border color – this also defines the visual color of the stamp
                    Color = Aspose.Pdf.Color.Red
                };

                // Set custom border thickness (width in points)
                // Border constructor requires the parent annotation instance
                stamp.Border = new Border(stamp) { Width = 2 };

                // Add the annotation to the page
                page.Annotations.Add(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}