using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF
        const string outputPdf = "output_with_link.pdf"; // result PDF
        const string url       = "https://example.com"; // target URL

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the link will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation first
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set properties that do not depend on the annotation instance
            link.Action = new GoToURIAction(url);
            link.Color  = Aspose.Pdf.Color.Blue;

            // Now that 'link' is instantiated, we can create a Border that references it
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page's collection
            page.Annotations.Add(link);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Link annotation added and saved to '{outputPdf}'.");
    }
}
