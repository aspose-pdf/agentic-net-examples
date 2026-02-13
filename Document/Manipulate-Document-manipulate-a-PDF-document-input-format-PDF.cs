using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class PdfManipulator
{
    static void Main(string[] args)
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The PDF contains no pages.");
                return;
            }

            // Get the first page to add an annotation
            Page page = pdfDocument.Pages[1];

            // Define a rectangle for the annotation (llx, lly, urx, ury)
            // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation that points to an external URL
            LinkAnnotation linkAnnot = new LinkAnnotation(page, rect)
            {
                Action = new GoToURIAction("https://www.example.com"),
                Color = Color.Blue,
                Contents = "Visit Example.com"
            };

            // Initialize the border for the annotation
            linkAnnot.Border = new Border(linkAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page
            page.Annotations.Add(linkAnnot);

            // Save the modified PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF manipulation completed successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
