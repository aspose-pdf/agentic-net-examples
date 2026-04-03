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

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create first text field (e.g., First Name)
            // -------------------------------------------------
            Aspose.Pdf.Rectangle rectFirst = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            TextBoxField firstNameField = new TextBoxField(page, rectFirst)
            {
                PartialName = "FirstName",   // field identifier
                Value = "John"               // initial value
            };
            // Add the field to the form on page 1
            doc.Form.Add(firstNameField, 1);

            // -------------------------------------------------
            // Create second text field (e.g., Last Name)
            // -------------------------------------------------
            Aspose.Pdf.Rectangle rectLast = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            TextBoxField lastNameField = new TextBoxField(page, rectLast)
            {
                PartialName = "LastName",
                Value = "Doe"
            };
            doc.Form.Add(lastNameField, 1);

            // -------------------------------------------------
            // Concatenate the values of the two text fields
            // -------------------------------------------------
            string concatenatedValue = firstNameField.Value.ToString() + lastNameField.Value.ToString();

            // -------------------------------------------------
            // Create a barcode field that will display the concatenated string
            // -------------------------------------------------
            Aspose.Pdf.Rectangle rectBarcode = new Aspose.Pdf.Rectangle(100, 550, 300, 600);
            BarcodeField barcodeField = new BarcodeField(page, rectBarcode)
            {
                PartialName = "FullNameBarcode"
            };
            // Generate a Code 128 barcode from the concatenated string
            barcodeField.AddBarcode(concatenatedValue);

            // Add the barcode field to the form on page 1
            doc.Form.Add(barcodeField, 1);

            // -------------------------------------------------
            // Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}