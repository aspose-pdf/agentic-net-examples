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

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for three text fields and one barcode field
            Aspose.Pdf.Rectangle rectField1 = new Aspose.Pdf.Rectangle(50, 750, 250, 770);
            Aspose.Pdf.Rectangle rectField2 = new Aspose.Pdf.Rectangle(50, 720, 250, 740);
            Aspose.Pdf.Rectangle rectField3 = new Aspose.Pdf.Rectangle(50, 690, 250, 710);
            Aspose.Pdf.Rectangle rectBarcode = new Aspose.Pdf.Rectangle(50, 640, 250, 680);

            // Create three regular text box fields
            TextBoxField txtField1 = new TextBoxField(page, rectField1) { Name = "Field1", PartialName = "Field1" };
            TextBoxField txtField2 = new TextBoxField(page, rectField2) { Name = "Field2", PartialName = "Field2" };
            TextBoxField txtField3 = new TextBoxField(page, rectField3) { Name = "Field3", PartialName = "Field3" };

            // Set initial values (could be left empty for user input)
            txtField1.Value = "ABC";
            txtField2.Value = "123";
            txtField3.Value = "XYZ";

            // Add the text fields to the document form
            doc.Form.Add(txtField1);
            doc.Form.Add(txtField2);
            doc.Form.Add(txtField3);

            // Concatenate the values of the three fields to form the barcode data
            string barcodeData = $"{txtField1.Value}{txtField2.Value}{txtField3.Value}";

            // Create a barcode field (Code128) and generate the barcode from the concatenated string
            BarcodeField barcodeField = new BarcodeField(page, rectBarcode) { Name = "BarcodeField", PartialName = "BarcodeField" };
            barcodeField.AddBarcode(barcodeData); // Generates the barcode and makes the field read‑only

            // Add the barcode field to the document form
            doc.Form.Add(barcodeField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}