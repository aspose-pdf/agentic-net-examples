using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "FormWithBarcode.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing fields)
            Page page = doc.Pages.Add();

            // ---------- Create regular text fields ----------
            // First text field
            TextBoxField txtField1 = new TextBoxField(page,
                new Aspose.Pdf.Rectangle(100, 700, 300, 730));
            txtField1.Name = "FirstName";
            txtField1.PartialName = "FirstName";
            txtField1.Value = "John";
            doc.Form.Add(txtField1, 1); // add to page 1

            // Second text field
            TextBoxField txtField2 = new TextBoxField(page,
                new Aspose.Pdf.Rectangle(100, 650, 300, 680));
            txtField2.Name = "LastName";
            txtField2.PartialName = "LastName";
            txtField2.Value = "Doe";
            doc.Form.Add(txtField2, 1); // add to page 1

            // ---------- Create barcode field ----------
            // The barcode will be placed below the text fields
            BarcodeField barcodeField = new BarcodeField(page,
                new Aspose.Pdf.Rectangle(100, 560, 300, 610));
            barcodeField.Name = "FullNameBarcode";
            barcodeField.PartialName = "FullNameBarcode";

            // Concatenate the values of the two text fields
            string concatenatedValue = $"{txtField1.Value}{txtField2.Value}";

            // Generate a Code128 barcode from the concatenated string
            // AddBarcode makes the field read‑only and renders the barcode
            barcodeField.AddBarcode(concatenatedValue);

            // Add the barcode field to the form on page 1
            doc.Form.Add(barcodeField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}