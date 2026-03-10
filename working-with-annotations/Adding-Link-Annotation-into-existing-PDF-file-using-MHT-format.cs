using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";
        const string outputPdf = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle area for the link annotation
            // (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Visit Aspose website"
            };

            // Set the action to open an external URL when the link is clicked
            link.Action = new GoToURIAction("https://www.aspose.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdf}'.");
    }
}