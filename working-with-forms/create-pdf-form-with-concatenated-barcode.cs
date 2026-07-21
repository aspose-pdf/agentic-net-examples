using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define rectangles for the form fields (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectFirstName = new Aspose.Pdf.Rectangle(50, 750, 250, 770);
            Aspose.Pdf.Rectangle rectLastName  = new Aspose.Pdf.Rectangle(50, 720, 250, 740);
            Aspose.Pdf.Rectangle rectBarcode   = new Aspose.Pdf.Rectangle(50, 650, 250, 690);

            // Create a text box field for First Name
            TextBoxField firstNameField = new TextBoxField(page, rectFirstName);
            firstNameField.PartialName = "FirstName";
            firstNameField.Value = "John";               // default value (can be changed later)
            doc.Form.Add(firstNameField);

            // Create a text box field for Last Name
            TextBoxField lastNameField = new TextBoxField(page, rectLastName);
            lastNameField.PartialName = "LastName";
            lastNameField.Value = "Doe";                 // default value (can be changed later)
            doc.Form.Add(lastNameField);

            // Create a barcode field that will hold the concatenated string
            BarcodeField barcodeField = new BarcodeField(page, rectBarcode);
            barcodeField.PartialName = "FullBarcode";

            // Concatenate the values of the two text fields
            string concatenatedValue = $"{firstNameField.Value}{lastNameField.Value}";

            // Generate the barcode (Code128 is the default symbology)
            barcodeField.AddBarcode(concatenatedValue);

            // Add the barcode field to the form
            doc.Form.Add(barcodeField);

            // Save the PDF with the form fields
            doc.Save("FormWithBarcode.pdf");
        }

        Console.WriteLine("PDF form with barcode field created successfully.");
    }
}