using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "product_form.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form
            Page page = doc.Pages.Add();

            // Define the position and size of the barcode field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box field that will hold the barcode image
            TextBoxField barcodeField = new TextBoxField(page, barcodeRect)
            {
                // Set the field name (used when extracting form data)
                PartialName = "ProductBarcode"
            };

            // Add the field to the document's form collection (page numbers are 1‑based)
            doc.Form.Add(barcodeField, 1);

            // Render the barcode into the field
            barcodeField.AddBarcode("PROD-0012345");

            // Optionally add a label describing the field
            TextFragment label = new TextFragment("Product Identifier:")
            {
                // Position the label near the barcode field
                Position = new Position(100, 660)
            };
            page.Paragraphs.Add(label);

            // Save the PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode field saved to '{outputPath}'.");
    }
}
