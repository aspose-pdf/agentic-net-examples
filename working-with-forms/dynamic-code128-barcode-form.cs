using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "BarcodeForm.pdf";

        // Create a new PDF document and add a page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Create a source text field (the field whose value drives the barcode).
            // ------------------------------------------------------------
            // Rectangle coordinates: lower‑left (llx, lly) and upper‑right (urx, ury).
            Aspose.Pdf.Rectangle srcRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField sourceField = new TextBoxField(page, srcRect)
            {
                PartialName = "SourceField",   // field name used in JavaScript
                Value = ""                     // initial empty value
            };
            doc.Form.Add(sourceField);

            // ------------------------------------------------------------
            // 2. Create a barcode field that will display a Code128 barcode.
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 560);
            BarcodeField barcodeField = new BarcodeField(page, barcodeRect)
            {
                PartialName = "BarcodeField"   // field name used in JavaScript
            };
            // Initialise the barcode field with an empty value.
            // AddBarcode sets the symbology to Code128 and makes the field read‑only.
            barcodeField.AddBarcode(string.Empty);
            doc.Form.Add(barcodeField);

            // ------------------------------------------------------------
            // 3. Add JavaScript to the source field so that any change updates the barcode.
            // ------------------------------------------------------------
            // The OnCalculate event fires when the field value is recalculated.
            // The script copies the current value (event.value) into the barcode field.
            sourceField.Actions.OnCalculate = new JavascriptAction(
                "this.getField('BarcodeField').value = event.value;");

            // ------------------------------------------------------------
            // 4. Save the PDF.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode form saved to '{outputPath}'.");
    }
}