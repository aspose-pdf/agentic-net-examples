using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for AnnotationFlags
using Aspose.Pdf.Forms;      // for ButtonField, TextBoxField

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithBarcode.pdf";
        const string numericId   = "1234567890"; // Example identifier

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page will be index 1)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Create a button form field that will represent the barcode
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            ButtonField barcodeField = new ButtonField(page, barcodeRect);
            barcodeField.PartialName = "BarcodeField"; // field name
            // Store the numeric identifier as the button's value (placeholder for a real barcode image)
            barcodeField.Value = numericId;
            // Ensure the barcode appears when printed
            barcodeField.Flags = AnnotationFlags.Print;
            // Add the button field to the document's AcroForm collection
            doc.Form.Add(barcodeField, 1);

            // ------------------------------------------------------------
            // Create a text box form field to hold the numeric identifier
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle idRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);
            TextBoxField idField = new TextBoxField(page, idRect);
            idField.PartialName = "Identifier";
            idField.Value = numericId; // Pre‑populate the field with the identifier
            doc.Form.Add(idField, 1);

            // ------------------------------------------------------------
            // Save the PDF form
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}
