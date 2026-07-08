using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction
using Aspose.Pdf.Drawing;   // Rectangle

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // PDF with at least one page
        const string outputPath = "barcode_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a source text box where the user will type the data.
            // -----------------------------------------------------------------
            // Rectangle(xLL, yLL, xUR, yUR) – coordinates are in points.
            Aspose.Pdf.Rectangle srcRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            TextBoxField sourceField = new TextBoxField(doc.Pages[1], srcRect)
            {
                PartialName = "sourceField",
                Contents    = "Enter value"
            };
            doc.Form.Add(sourceField);

            // -----------------------------------------------------------------
            // 2. Create a barcode field that will display Code128.
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            BarcodeField barcodeField = new BarcodeField(doc.Pages[1], barcodeRect)
            {
                PartialName = "barcodeField",
                ReadOnly    = true   // AddBarcode makes it read‑only; set explicitly for clarity
            };
            doc.Form.Add(barcodeField);

            // Initialise the barcode with an empty value.
            // AddBarcode generates a Code128 barcode from the field's value.
            barcodeField.AddBarcode(string.Empty);

            // -----------------------------------------------------------------
            // 3. Link the source field to the barcode field via JavaScript.
            //    When the source field is formatted (e.g., on exit), the script
            //    copies its value into the barcode field. The barcode field
            //    automatically re‑renders because its value changed.
            // -----------------------------------------------------------------
            // The JavascriptAction class lives in Aspose.Pdf.Annotations.
            // 'event.value' holds the current value of the source field.
            sourceField.Actions.OnFormat = new JavascriptAction(
                "var bc = this.getField('barcodeField');" +
                "bc.value = event.value;" // update barcode field value
            );

            // -----------------------------------------------------------------
            // 4. Save the modified PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic Code128 barcode saved to '{outputPath}'.");
    }
}