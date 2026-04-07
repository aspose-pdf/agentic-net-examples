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

        // Load the PDF document inside a using block for proper disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Enable automatic recalculation of form fields (helps with JavaScript updates)
            doc.Form.AutoRecalculate = true;

            // Names of the fields – adjust these to match the actual field names in your PDF
            const string sourceFieldName  = "sourceField";
            const string barcodeFieldName = "barcodeField";

            // Retrieve the source text field
            Aspose.Pdf.Forms.TextBoxField sourceField = doc.Form[sourceFieldName] as Aspose.Pdf.Forms.TextBoxField;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Source field '{sourceFieldName}' not found or is not a TextBoxField.");
                return;
            }

            // Retrieve (or create) the barcode field
            Aspose.Pdf.Forms.BarcodeField barcodeField = doc.Form[barcodeFieldName] as Aspose.Pdf.Forms.BarcodeField;
            if (barcodeField == null)
            {
                // If the barcode field does not exist, create it on page 1 at a default location
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                barcodeField = new Aspose.Pdf.Forms.BarcodeField(doc.Pages[1], rect);
                barcodeField.PartialName = barcodeFieldName;
                doc.Form.Add(barcodeField);
            }

            // Initialize the barcode with a placeholder value (Code128 is the default for AddBarcode)
            barcodeField.AddBarcode("PLACEHOLDER");

            // JavaScript that copies the source field's value into the barcode field whenever it changes
            // The script runs on the source field's "On Blur" event (when the field loses focus)
            string js = $"var bc = this.getField('{barcodeFieldName}'); bc.value = event.value;";

            // Attach the JavaScript action to the source field
            sourceField.ExecuteFieldJavaScript(new Aspose.Pdf.Annotations.JavascriptAction(js));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode field set to Code128 and linked to source field. Saved as '{outputPath}'.");
    }
}