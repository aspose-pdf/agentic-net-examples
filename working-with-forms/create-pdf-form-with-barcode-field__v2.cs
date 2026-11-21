using System;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

// Alias to avoid ambiguity with System.Drawing.Rectangle
using PdfRect = Aspose.Pdf.Rectangle;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode_form.pdf";
        const string barcodeFieldName = "BarcodeField";
        const string numericId = "1234567890";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the position and size of the barcode field (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            PdfRect rect = new PdfRect(100, 600, 300, 650);

            // Create a text box field that will hold the barcode
            TextBoxField barcodeField = new TextBoxField(page, rect);
            barcodeField.PartialName = barcodeFieldName; // Field name

            // Visual styling – Border needs the field as its parent, background is set via Characteristics
            barcodeField.Border = new Border(barcodeField) { Width = 1 };
            barcodeField.Characteristics.Background = System.Drawing.Color.LightGray;

            // Add the field to the document's AcroForm (page number is 1 because we have a single page)
            doc.Form.Add(barcodeField, 1);

            // Draw the barcode into the field (Code128 is the default symbology for AddBarcode)
            barcodeField.AddBarcode(numericId);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}
