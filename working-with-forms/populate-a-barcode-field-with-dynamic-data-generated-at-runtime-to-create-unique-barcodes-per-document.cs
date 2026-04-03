using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Forms;          // Form fields (BarcodeField)
using Aspose.Pdf.Drawing;        // For Rectangle (if needed, but we use fully qualified)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePath = "template.pdf";
        const string outputPath   = "output.pdf";

        // Verify template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use Document constructor with load)
        using (Document pdfDoc = new Document(templatePath))
        {
            // Generate a unique barcode value at runtime (e.g., GUID without hyphens)
            string uniqueCode = Guid.NewGuid().ToString("N");

            // Define the barcode field rectangle (left, bottom, right, top)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the first page of the document
            // Constructor (Document, Rectangle) adds the field to page 1 automatically
            BarcodeField barcodeField = new BarcodeField(pdfDoc, barcodeRect)
            {
                // Optional: set a name for the field (useful for later reference)
                Name = "UniqueBarcode"
            };

            // Populate the field with the generated code.
            // AddBarcode creates a Code128 barcode and makes the field read‑only.
            barcodeField.AddBarcode(uniqueCode);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode saved to '{outputPath}'.");
    }
}