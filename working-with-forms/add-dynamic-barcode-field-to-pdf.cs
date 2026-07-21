using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // existing PDF with or without a barcode field
        const string outputPath = "barcode_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Generate a unique value for the barcode (e.g., a GUID without hyphens)
        string uniqueCode = Guid.NewGuid().ToString("N");

        // Load the PDF, add a barcode field, set its value, and save
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the barcode will be placed (llx, lly, urx, ury)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the first page
            BarcodeField barcodeField = new BarcodeField(doc.Pages[1], rect);

            // Add a Code128 barcode with the generated unique code
            barcodeField.AddBarcode(uniqueCode);

            // Optionally set a name for the field (useful for later reference)
            barcodeField.Name = "UniqueBarcode";

            // Add the field to the page's annotations collection
            doc.Pages[1].Annotations.Add(barcodeField);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode field populated with value '{uniqueCode}' and saved to '{outputPath}'.");
    }
}