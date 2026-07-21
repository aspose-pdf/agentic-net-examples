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

        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Define the rectangle for the circle annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a circle annotation on page 2
            CircleAnnotation circle = new CircleAnnotation(doc.Pages[2], rect);

            // Set the interior (fill) color to green
            circle.InteriorColor = Aspose.Pdf.Color.Green;

            // Set the border width to 3 points
            circle.Border = new Border(circle) { Width = 3 };

            // Add the annotation to the page's annotation collection
            doc.Pages[2].Annotations.Add(circle);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}