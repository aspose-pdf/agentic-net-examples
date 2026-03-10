using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // required for XpsSaveOptions if available

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // existing PDF
        const string outputPath    = "output.xps";     // result in XPS format

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, add a link annotation, and save as XPS
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least two pages for the link target
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF must contain at least two pages.");
                return;
            }

            // Choose the source page where the clickable rectangle will be placed
            Page sourcePage = doc.Pages[1];   // 1‑based indexing

            // Define the rectangle area for the link (left, bottom, right, top)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation on the chosen page
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect)
            {
                // Set the visual appearance of the link rectangle
                Color = Aspose.Pdf.Color.Blue
            };

            // Define the action: navigate to page 2 of the same document
            link.Action = new GoToAction(doc.Pages[2]);

            // Add the annotation to the page's annotation collection
            sourcePage.Annotations.Add(link);

            // Save the modified document as XPS.
            // Non‑PDF formats require explicit SaveOptions (rule: save-to-non-pdf-always-use-save-options).
            // XpsSaveOptions resides in Aspose.Pdf.Facades namespace.
            XpsSaveOptions xpsOptions = new XpsSaveOptions();
            doc.Save(outputPath, xpsOptions);
        }

        Console.WriteLine($"Link annotation added and PDF saved as XPS to '{outputPath}'.");
    }
}