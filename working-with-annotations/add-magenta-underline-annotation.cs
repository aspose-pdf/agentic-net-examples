using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF or create a new one
        const string outputPath = "output.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPath))
        {
            using (Document newDoc = new Document())
            {
                newDoc.Pages.Add(); // add a single blank page
                newDoc.Save(inputPath);
            }
        }

        // Load the PDF, add an underline annotation, set its color and thickness, then save
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
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

        Console.WriteLine($"Underline annotation added. Saved to '{outputPath}'.");
    }
}