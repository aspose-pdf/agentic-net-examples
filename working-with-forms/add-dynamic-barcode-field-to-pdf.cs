using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF (template) and output PDF paths
        const string inputPath  = "template.pdf";
        const string outputPath = "barcode_filled.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a unique barcode value (e.g., GUID without hyphens)
        string uniqueCode = Guid.NewGuid().ToString("N");

        // Load the PDF, add a barcode field, populate it, and save
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the barcode will be placed (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle for the barcode field (left, bottom, width, height)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the barcode field on the selected page
            BarcodeField barcodeField = new BarcodeField(page, rect);

            // Add a Code128 barcode using the generated unique code
            // This method also makes the field read‑only automatically
            barcodeField.AddBarcode(uniqueCode);

            // Optionally set a visible border color for the field
            barcodeField.Color = Aspose.Pdf.Color.Black;

            // Add the field to the page's annotations collection
            page.Annotations.Add(barcodeField);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode PDF created: {outputPath}");
    }
}