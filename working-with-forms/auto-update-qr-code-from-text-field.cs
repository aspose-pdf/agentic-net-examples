using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF containing the form fields
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text field that will drive the QR code
            TextBoxField textField = doc.Form["TextField"] as TextBoxField;
            // Retrieve the QR‑code field – it is also a TextBoxField because QR codes are drawn into a text field
            TextBoxField qrField = doc.Form["QrCodeField"] as TextBoxField;

            if (textField == null || qrField == null)
            {
                Console.Error.WriteLine("Required form fields not found.");
                return;
            }

            // Make the QR field a calculated field that mirrors the text field's value.
            // The calculation script runs in the PDF viewer and updates the field value.
            string calcScript = $"event.value = this.getField('{textField.Name}').value;";
            qrField.Actions.OnCalculate = new JavascriptAction(calcScript);

            // Add QR‑code rendering to the QR field. The barcode will be generated from the field's value
            // each time the field is displayed.
            qrField.AddBarcode(qrField.Value ?? string.Empty);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑updating QR code: {outputPath}");
    }
}
