using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;   // for Border class

// Load a CGM file (input‑only format) and add a decorated link annotation.
// The result is saved as PDF because CGM cannot be saved.
class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        // Load the CGM document using the appropriate load options.
        using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The CGM file did not produce any pages.");
                return;
            }

            // Choose the first page to place the annotation.
            Page page = doc.Pages[1];

            // Define the rectangle for the link annotation.
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation.
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Visual decoration.
                Color = Aspose.Pdf.Color.Blue,          // Annotation border color.
                Contents = "Visit Aspose.Pdf website", // Tooltip text.
            };

            // Set a border with a specific width.
            // Border requires the parent annotation in its constructor.
            link.Border = new Border(link) { Width = 2 };

            // Define the action – an external URL.
            // Hyperlink property is not a string; use GoToURIAction instead.
            link.Action = new GoToURIAction("https://www.aspose.com/pdf");

            // Add the annotation to the page.
            page.Annotations.Add(link);

            // Save the modified document as PDF (CGM cannot be saved).
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with decorated link annotation saved to '{outputPdfPath}'.");
    }
}