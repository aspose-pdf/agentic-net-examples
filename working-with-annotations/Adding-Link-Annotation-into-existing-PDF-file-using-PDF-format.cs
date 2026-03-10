using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                // Tooltip text shown when hovering
                Contents = "Visit Aspose website"
            };

            // Set the action – external URL using GoToURIAction (the class resides in Aspose.Pdf.Annotations)
            link.Action = new GoToURIAction("https://www.aspose.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added and saved to '{outputPath}'.");
    }
}