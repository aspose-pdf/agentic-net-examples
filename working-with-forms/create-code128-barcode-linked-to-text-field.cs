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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the source field already exists in the form and is named "SourceField"
            // Retrieve it (cast to TextBoxField because we need its Actions collection)
            TextBoxField sourceField = doc.Form["SourceField"] as TextBoxField;
            if (sourceField == null)
            {
                Console.Error.WriteLine("Source field 'SourceField' not found.");
                return;
            }

            // Create a barcode field on the first page
            Page page = doc.Pages[1];
            // Define the rectangle where the barcode will be placed
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            BarcodeField barcodeField = new BarcodeField(page, barcodeRect)
            {
                PartialName = "BarcodeField",   // field name
                ReadOnly    = true             // barcode fields are read‑only by design
            };

            // Add the barcode field to the document's form and page annotations
            doc.Form.Add(barcodeField);
            page.Annotations.Add(barcodeField);

            // Initialise the barcode with the current value of the source field
            // AddBarcode generates a Code‑128 barcode from the supplied string
            barcodeField.AddBarcode(sourceField.Value ?? string.Empty);

            // Attach a JavaScript action to the source field so that any change updates the barcode
            // The OnFormat action runs when the field value is formatted (i.e., after user input)
            // It copies the new value into the barcode field; the barcode field will redraw automatically
            string jsCode = @"
                var src = event.value;
                var bc  = this.getField('BarcodeField');
                bc.value = src;
            ";
            sourceField.Actions.OnFormat = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode field added and linked. Output saved to '{outputPath}'.");
    }
}