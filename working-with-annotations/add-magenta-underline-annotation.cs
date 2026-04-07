using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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
            // Define the rectangle where the underline will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the underline annotation on the first page
            UnderlineAnnotation underline = new UnderlineAnnotation(doc.Pages[1], rect);

            // Set the annotation color to magenta
            underline.Color = Aspose.Pdf.Color.Magenta;

            // Set the line thickness to 2 points via the Border property
            // Border requires the parent annotation in its constructor
            underline.Border = new Border(underline) { Width = 2 };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(underline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
    }
}