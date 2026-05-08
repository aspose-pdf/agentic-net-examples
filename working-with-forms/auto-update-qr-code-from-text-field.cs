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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Create a text box field (the source field)
            // -------------------------------------------------
            // Position and size of the text field
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            TextBoxField txtField = new TextBoxField(doc, txtRect);
            txtField.Name = "txtSource";
            txtField.PartialName = "txtSource";
            // Optional visual settings
            txtField.Border = new Border(txtField) { Width = 1 };
            txtField.Color = Aspose.Pdf.Color.LightGray;
            // Add the text field to the first page
            doc.Pages[1].Annotations.Add(txtField);

            // -------------------------------------------------
            // 2. Create a QR code barcode field (the target field)
            // -------------------------------------------------
            // Position and size of the QR code field
            Aspose.Pdf.Rectangle qrRect = new Aspose.Pdf.Rectangle(350, 650, 500, 800);
            BarcodeField qrField = new BarcodeField(doc, qrRect);
            qrField.Name = "qrCode";
            qrField.PartialName = "qrCode";
            // Make the field read‑only; its value will be set by JavaScript
            qrField.ReadOnly = true;
            // Optional visual settings
            qrField.Border = new Border(qrField) { Width = 1 };
            qrField.Color = Aspose.Pdf.Color.LightGray;
            // Add the barcode field to the first page
            doc.Pages[1].Annotations.Add(qrField);

            // -------------------------------------------------
            // 3. Attach JavaScript to the text field so that
            //    the QR code updates whenever the text changes.
            // -------------------------------------------------
            // The JavaScript runs on each character modification.
            // It copies the current value of the text field into the QR code field.
            string js = "this.getField('qrCode').value = this.value;";
            txtField.Actions.OnModifyCharacter = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Ensure automatic recalculation (optional but safe)
            // -------------------------------------------------
            doc.Form.AutoRecalculate = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
