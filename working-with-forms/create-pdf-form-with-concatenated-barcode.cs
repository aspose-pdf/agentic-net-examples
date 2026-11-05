using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Form field types and BarcodeType

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithBarcode.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define positions for the form fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectFirstName = new Aspose.Pdf.Rectangle(50, 750, 250, 770);
            Aspose.Pdf.Rectangle rectLastName  = new Aspose.Pdf.Rectangle(50, 720, 250, 740);
            Aspose.Pdf.Rectangle rectId        = new Aspose.Pdf.Rectangle(50, 690, 250, 710);
            Aspose.Pdf.Rectangle rectBarcode   = new Aspose.Pdf.Rectangle(50, 620, 300, 660);

            // Create TextBox fields for First Name, Last Name and ID
            TextBoxField firstNameField = new TextBoxField(page, rectFirstName);
            firstNameField.PartialName = "FirstName";
            firstNameField.Value = ""; // initial empty value
            doc.Form.Add(firstNameField, 1);

            TextBoxField lastNameField = new TextBoxField(page, rectLastName);
            lastNameField.PartialName = "LastName";
            lastNameField.Value = "";
            doc.Form.Add(lastNameField, 1);

            TextBoxField idField = new TextBoxField(page, rectId);
            idField.PartialName = "ID";
            idField.Value = "";
            doc.Form.Add(idField, 1);

            // Create a Barcode field
            BarcodeField barcodeField = new BarcodeField(page, rectBarcode);
            barcodeField.PartialName = "MyBarcode";
            // The BarcodeField.Type property does not exist in the current Aspose.Pdf version.
            // If a specific symbology is required, use the BarcodeField.BarcodeType property where available.
            // For maximum compatibility we rely on the default symbology (Code128) or set it conditionally:
            // if (barcodeField.GetType().GetProperty("BarcodeType") != null)
            //     barcodeField.BarcodeType = BarcodeType.Code128;
            barcodeField.Value = ""; // will be set after concatenation
            doc.Form.Add(barcodeField, 1);

            // ----- Populate the text fields (simulating user input) -----
            firstNameField.Value = "John";
            lastNameField.Value = "Doe";
            idField.Value = "12345";

            // Concatenate the values of the three text fields
            string concatenatedValue = firstNameField.Value + lastNameField.Value + idField.Value;

            // Assign the concatenated string to the barcode field
            barcodeField.Value = concatenatedValue;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}
