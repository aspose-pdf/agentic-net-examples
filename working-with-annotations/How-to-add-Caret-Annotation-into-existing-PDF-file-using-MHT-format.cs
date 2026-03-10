using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

            // Get the first page (1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle for the caret annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the caret annotation on the specified page
            CaretAnnotation caret = new CaretAnnotation(page, rect)
            {
                // Optional visual and textual properties
                Color    = Aspose.Pdf.Color.Red,
                Contents = "Caret annotation example"
                // Symbol property is optional; omitted to avoid compatibility issues
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(caret);

            // Save the modified document as PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Caret annotation added and saved to '{outputPdfPath}'.");
    }
}
