using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xslFoPath   = "input.xslfo";   // XSL‑FO source file
        const string outputPdf   = "output.pdf";    // Resulting PDF file
        const string linkUrl     = "https://www.example.com";
        const int    targetPage  = 1;               // Page where the link will be placed (1‑based)

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        try
        {
            // Load XSL‑FO and create a PDF document
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();
            using (Document pdfDoc = new Document(xslFoPath, loadOptions))
            {
                // Ensure the target page exists
                if (pdfDoc.Pages.Count < targetPage)
                {
                    Console.Error.WriteLine($"Document has only {pdfDoc.Pages.Count} page(s).");
                    return;
                }

                // Define the rectangle for the link annotation (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the link annotation on the specified page
                Page page = pdfDoc.Pages[targetPage];
                LinkAnnotation link = new LinkAnnotation(page, linkRect)
                {
                    Color = Aspose.Pdf.Color.Blue,          // Visual border color
                    Contents = "Visit Example.com",         // Tooltip text
                };

                // Set the action to open an external URL
                link.Action = new GoToURIAction(linkUrl);

                // Add the annotation to the page
                page.Annotations.Add(link);

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with link annotation saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}