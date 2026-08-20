using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_barcode.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Assume there is an existing text field named "SourceField"
            TextBoxField sourceField = doc.Form["SourceField"] as TextBoxField;
            if (sourceField == null)
            {
                Console.Error.WriteLine("Source field 'SourceField' not found.");
                return;
            }

            // Create a barcode field on the first page
            Page page = doc.Pages[1];
            // Define the rectangle where the barcode will appear
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            BarcodeField barcodeField = new BarcodeField(page, barcodeRect)
            {
                // Set a unique name for the barcode field
                Name = "BarcodeField",
                // Make the field read‑only (barcode fields are read‑only by default after AddBarcode)
                ReadOnly = true
            };

            // Add the barcode field to the document's form collection
            doc.Form.Add(barcodeField);

            // JavaScript that updates the barcode value whenever the source field changes
            // The script copies the source field's value into this field (the barcode field)
            // and then calls AddBarcode to regenerate the barcode.
            string js = @"
                var src = this.getField('SourceField').value;
                this.value = src;
                this.addBarcode(src);
            ";
            barcodeField.Actions.OnFormat = new JavascriptAction(js);

            // Initialize the barcode with the current value of the source field
            string initialValue = sourceField.Value?.ToString() ?? string.Empty;
            barcodeField.AddBarcode(initialValue);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode PDF saved to '{outputPath}'.");
    }
}