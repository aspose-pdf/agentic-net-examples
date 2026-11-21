using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Define a named destination called "MyTarget" on page 2.
            // The destination points to the top‑left corner of the page.
            // ------------------------------------------------------------
            // XYZExplicitDestination(page, x, y, zoom)
            // x = 0 (left), y = page height (top), zoom = 1 (100%)
            var page2 = doc.Pages[2];
            var topLeftDest = new XYZExplicitDestination(page2, 0, page2.MediaBox.Height, 1);
            doc.NamedDestinations.Add("MyTarget", topLeftDest);

            // ------------------------------------------------------------
            // Create a link annotation on page 1 that jumps to "MyTarget".
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            var link = new LinkAnnotation(doc.Pages[1], linkRect)
            {
                Action = new GoToAction(doc, "MyTarget"),
                Color = Aspose.Pdf.Color.Blue // optional visual styling
            };
            doc.Pages[1].Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Saved to '{outputPath}'.");
    }
}
