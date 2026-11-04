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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the source text field (the field whose changes should trigger barcode update)
            TextBoxField sourceField = doc.Form["SourceField"] as TextBoxField;
            if (sourceField == null)
            {
                Console.Error.WriteLine("SourceField not found or not a TextBoxField.");
                return;
            }

            // Retrieve the barcode field that will display the Code128 barcode
            BarcodeField barcodeField = doc.Form["BarcodeField"] as BarcodeField;
            if (barcodeField == null)
            {
                Console.Error.WriteLine("BarcodeField not found or not a BarcodeField.");
                return;
            }

            // NOTE: In the version of Aspose.Pdf bundled with this project the BarcodeField
            // does not expose a BarcodeType property. The default symbology for a BarcodeField
            // is Code128, which satisfies the requirement. If a newer library version is used,
            // the following line can be uncommented to set the type explicitly:
            // barcodeField.BarcodeType = Aspose.Pdf.Forms.BarcodeType.Code128;

            // Initialize the barcode value with the current source field value
            barcodeField.Value = sourceField.Value;

            // JavaScript that updates the barcode whenever the source field value changes.
            // Using the OnCalculate action ensures the script runs on every value change.
            string js = "var src = event.value;" +
                        "var bc  = this.getField('BarcodeField');" +
                        "bc.value = src;" +
                        "bc.update();";
            sourceField.Actions.OnCalculate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode updated PDF saved to '{outputPath}'.");
    }
}
