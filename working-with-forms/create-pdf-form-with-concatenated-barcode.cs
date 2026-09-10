using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "FormWithBarcode.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (coordinates: llx, lly, urx, ury)
            // Field 1
            Aspose.Pdf.Rectangle rectField1 = new Aspose.Pdf.Rectangle(50, 750, 250, 770);
            // Field 2
            Aspose.Pdf.Rectangle rectField2 = new Aspose.Pdf.Rectangle(50, 720, 250, 740);
            // Barcode field
            Aspose.Pdf.Rectangle rectBarcode = new Aspose.Pdf.Rectangle(50, 660, 250, 720);

            // Create first text box field
            TextBoxField field1 = new TextBoxField(page, rectField1)
            {
                Name = "FirstName",
                PartialName = "FirstName",
                Value = "John"
            };

            // Create second text box field
            TextBoxField field2 = new TextBoxField(page, rectField2)
            {
                Name = "LastName",
                PartialName = "LastName",
                Value = "Doe"
            };

            // Add the text fields to the form (page index is 1‑based)
            doc.Form.Add(field1, 1);
            doc.Form.Add(field2, 1);

            // Create a barcode field (Code128)
            BarcodeField barcodeField = new BarcodeField(page, rectBarcode)
            {
                Name = "FullNameBarcode",
                PartialName = "FullNameBarcode"
            };

            // Concatenate the values of the two text fields
            string concatenated = $"{field1.Value}{field2.Value}";

            // Generate the barcode from the concatenated string
            barcodeField.AddBarcode(concatenated);

            // Add the barcode field to the form
            doc.Form.Add(barcodeField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}