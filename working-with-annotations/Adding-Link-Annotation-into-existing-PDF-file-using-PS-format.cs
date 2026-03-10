using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";
        // Output PDF path (PS output is not supported; saving as PDF instead)
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF, modify it, and save
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the link will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                // Tooltip text shown when hovering
                Contents = "Go to Example.com"
            };

            // Set the action to open an external URL
            // (Hyperlink property is not a string; use GoToURIAction)
            link.Action = new GoToURIAction("https://www.example.com");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified document.
            // PS (PostScript) output is not supported by Aspose.Pdf;
            // the document must be saved as PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Link annotation added and saved to '{outputPdf}'.");
    }
}