using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToURIAction
using Aspose.Pdf.Text;   // not needed here but included for completeness

class Program
{
    static void Main()
    {
        // Input PostScript file (PS is supported only as input)
        const string inputPsPath  = "sample.ps";
        // Output PDF file (PS cannot be saved; we save as PDF)
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPsPath}");
            return;
        }

        // Load the PS file using PsLoadOptions inside a using block for deterministic disposal
        using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Choose the first page to place the link annotation
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the LinkAnnotation on the selected page
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Visit Aspose website"
            };

            // Set the action to open an external URL (use GoToURIAction, not Hyperlink string)
            link.Action = new GoToURIAction("https://www.aspose.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified document as PDF (PS cannot be used as an output format)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Link annotation added. PDF saved to '{outputPdfPath}'.");
    }
}