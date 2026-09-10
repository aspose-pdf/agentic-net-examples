using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the stamp will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a rubber‑stamp annotation on the first page
            StampAnnotation stamp = new StampAnnotation(doc.Pages[1], stampRect)
            {
                // Optional text shown when the annotation is selected
                Contents = "Important Section"
            };

            // Set the border color
            stamp.Color = Aspose.Pdf.Color.Red;

            // Set custom border thickness (requires a Border instance with the parent annotation)
            stamp.Border = new Border(stamp) { Width = 3 };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}