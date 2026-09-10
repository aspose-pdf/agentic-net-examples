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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the underline annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the underline annotation
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect)
            {
                // Set the annotation color to magenta
                Color = Aspose.Pdf.Color.Magenta
            };

            // Set the border (thickness) to 2 points
            underline.Border = new Border(underline) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(underline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
    }
}