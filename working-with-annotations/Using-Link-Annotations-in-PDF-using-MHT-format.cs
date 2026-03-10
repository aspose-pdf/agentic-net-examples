using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToAction if needed
using Aspose.Pdf.Text;   // not required here but kept for completeness

class Program
{
    static void Main()
    {
        // Paths for input MHT file and output PDF file
        const string inputMhtPath  = "input.mht";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputMhtPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputMhtPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(inputMhtPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Create a LinkAnnotation on the first page
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // NOTE: The LinkAnnotation instance must be created before it can be referenced in the Border constructor.
            LinkAnnotation link = new LinkAnnotation(pdfDoc.Pages[1], linkRect);
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Example 1: External URL link using GoToURIAction
            link.Action = new GoToURIAction("https://www.example.com");

            // Example 2: Internal link to page 2 (if the document has a second page)
            if (pdfDoc.Pages.Count >= 2)
            {
                // Use explicit destination (Fit) for the target page
                link.Destination = new FitExplicitDestination(pdfDoc.Pages[2]);
                // Uncomment the line below to use the internal link instead of the external URL
                // link.Action = null; // Ensure Action is not set when Destination is used
            }

            // Add the annotation to the page's annotation collection
            pdfDoc.Pages[1].Annotations.Add(link);

            // Save the modified document as PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdfPath}'.");
    }
}
