using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithBarcode.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectField1 = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            Aspose.Pdf.Rectangle rectField2 = new Aspose.Pdf.Rectangle(100, 650, 300, 670);
            Aspose.Pdf.Rectangle rectBarcode = new Aspose.Pdf.Rectangle(100, 550, 300, 620);

            // Create first text box field
            TextBoxField field1 = new TextBoxField(doc, rectField1)
            {
                Name = "FirstName",
                PartialName = "FirstName",
                Value = "John"
            };
            // Add the field to the form on page 1
            doc.Form.Add(field1, 1);

            // Create second text box field
            TextBoxField field2 = new TextBoxField(doc, rectField2)
            {
                Name = "LastName",
                PartialName = "LastName",
                Value = "Doe"
            };
            doc.Form.Add(field2, 1);

            // Create a barcode field
            BarcodeField barcodeField = new BarcodeField(page, rectBarcode)
            {
                Name = "FullNameBarcode",
                PartialName = "FullNameBarcode"
            };
            doc.Form.Add(barcodeField, 1);

            // Concatenate the values of the two text fields
            string concatenatedValue = $"{field1.Value}{field2.Value}";

            // Generate the barcode (Code128) from the concatenated string
            barcodeField.AddBarcode(concatenatedValue);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}