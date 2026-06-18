using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF containing a text field
        const string outputPath = "output_barcode.pdf"; // result PDF with barcode field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Locate (or create) the source text field whose value will drive the barcode.
            // ------------------------------------------------------------
            // Assume a text field named "SourceField" already exists.
            // If it does not exist, create it for demonstration purposes.
            TextBoxField sourceField = doc.Form["SourceField"] as TextBoxField;
            if (sourceField == null)
            {
                // Create a new text field on the first page
                sourceField = new TextBoxField(doc.Pages[1],
                    new Aspose.Pdf.Rectangle(100, 700, 300, 730));
                sourceField.PartialName = "SourceField";
                sourceField.Contents = "Enter value";
                doc.Form.Add(sourceField);
            }

            // ------------------------------------------------------------
            // 2. Create a BarcodeField (Code128) on the same page.
            // ------------------------------------------------------------
            BarcodeField barcodeField = new BarcodeField(doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 600, 300, 650));
            barcodeField.PartialName = "BarcodeField";

            // Initialise the barcode with an empty value; AddBarcode will render Code128.
            barcodeField.AddBarcode(string.Empty); // makes the field read‑only automatically

            // Add the barcode field to the form
            doc.Form.Add(barcodeField);

            // ------------------------------------------------------------
            // 3. Attach JavaScript to the source field so that any change updates the barcode.
            // ------------------------------------------------------------
            // The JavaScript runs when the source field is formatted (i.e., when its value changes).
            // It sets the barcode field's value to the source field's value, which triggers a redraw.
            string js = @"
                var src = event.value;
                var barcode = this.getField('BarcodeField');
                barcode.value = src;
            ";
            sourceField.Actions.OnFormat = new JavascriptAction(js);

            // ------------------------------------------------------------
            // 4. Save the modified document.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode PDF saved to '{outputPath}'.");
    }
}