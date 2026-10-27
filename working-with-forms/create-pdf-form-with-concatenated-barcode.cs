using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_barcode.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // ----- Create text fields -----
            // First name field
            TextBoxField firstName = new TextBoxField(page, new Aspose.Pdf.Rectangle(50, 750, 250, 730));
            firstName.PartialName = "FirstName";
            firstName.Value = "John";
            doc.Form.Add(firstName);

            // Last name field
            TextBoxField lastName = new TextBoxField(page, new Aspose.Pdf.Rectangle(50, 710, 250, 690));
            lastName.PartialName = "LastName";
            lastName.Value = "Doe";
            doc.Form.Add(lastName);

            // ID field
            TextBoxField idField = new TextBoxField(page, new Aspose.Pdf.Rectangle(50, 670, 250, 650));
            idField.PartialName = "ID";
            idField.Value = "12345";
            doc.Form.Add(idField);

            // ----- Generate concatenated data for the barcode -----
            string barcodeData = $"{firstName.Value}{lastName.Value}{idField.Value}";

            // NOTE: The Aspose.Pdf.Barcode namespace is not available in the current
            // library reference. As a fallback, we place the concatenated string into
            // a read‑only text field that can later be replaced with an actual barcode
            // image by a post‑processing step if required.
            TextBoxField barcodeField = new TextBoxField(page, new Aspose.Pdf.Rectangle(50, 620, 250, 580));
            barcodeField.PartialName = "BarcodeField";
            barcodeField.Value = barcodeData;
            barcodeField.ReadOnly = true; // make it behave like a barcode placeholder
            doc.Form.Add(barcodeField);

            // Save the PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode placeholder saved to '{outputPath}'.");
    }
}
