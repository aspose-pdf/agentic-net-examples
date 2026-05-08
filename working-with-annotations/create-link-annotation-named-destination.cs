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

        // Load the PDF (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Define a named destination called "MyDest"
            //    that points to page 2 (fit the whole page)
            // -------------------------------------------------
            // Add the named destination directly via the collection.
            // The collection expects a name and a destination object.
            doc.NamedDestinations.Add("MyDest", new FitExplicitDestination(doc.Pages[2]));

            // -------------------------------------------------
            // 2. Create a link annotation on page 1
            // -------------------------------------------------
            // Define the rectangle area for the link
            var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            // Create the annotation and configure it
            var link = new LinkAnnotation(doc.Pages[1], rect)
            {
                Action = new GoToAction(doc, "MyDest"),
                Color = Aspose.Pdf.Color.Blue
            };
            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(link);

            // -------------------------------------------------
            // 3. Save the modified PDF (lifecycle rule: use Save)
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}
