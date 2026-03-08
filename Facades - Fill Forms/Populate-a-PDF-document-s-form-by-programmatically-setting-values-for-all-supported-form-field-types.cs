using System;
using System.IO;
using Aspose.Pdf.Facades;

class PopulatePdfForm
{
    static void Main()
    {
        // Paths to the source PDF form and the output PDF.
        const string srcPdfPath = "FormTemplate.pdf";
        const string outPdfPath = "FormFilled.pdf";

        // Ensure the source file exists.
        if (!File.Exists(srcPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {srcPdfPath}");
            return;
        }

        // Open the PDF form using the Facade. The Form class implements IDisposable,
        // so we wrap it in a using block to guarantee proper resource release.
        using (Form form = new Form(srcPdfPath))
        {
            // ------------------------------------------------------------
            // Fill a simple text field.
            // ------------------------------------------------------------
            // Field name must be the fully qualified name as it appears in the PDF.
            form.FillField("CustomerName", "John Doe");

            // ------------------------------------------------------------
            // Fill a check box field.
            // ------------------------------------------------------------
            // Pass true to check the box, false to leave it unchecked.
            form.FillField("SubscribeNewsletter", true);

            // ------------------------------------------------------------
            // Fill a radio button group.
            // ------------------------------------------------------------
            // The index corresponds to the position of the option in the group
            // (zero‑based). Here we select the third option (index = 2).
            form.FillField("Gender", 2);

            // ------------------------------------------------------------
            // Fill a combo box (drop‑down list) field.
            // ------------------------------------------------------------
            // The index selects the item by its order in the list.
            form.FillField("CountryCombo", 4); // selects the 5th country in the list

            // ------------------------------------------------------------
            // Fill a list box field with multiple selections.
            // ------------------------------------------------------------
            // Provide an array of the exact visible values to select.
            string[] selectedItems = new[] { "Item A", "Item C", "Item D" };
            form.FillField("MultiSelectList", selectedItems);

            // ------------------------------------------------------------
            // Fill a barcode field.
            // ------------------------------------------------------------
            // The barcode field type must already exist in the PDF.
            form.FillBarcodeField("OrderBarcode", "1234567890");

            // ------------------------------------------------------------
            // Fill an image button field.
            // ------------------------------------------------------------
            // The image is supplied as a stream (e.g., a PNG file).
            using (FileStream imgStream = File.OpenRead("Signature.png"))
            {
                form.FillImageField("SignatureImage", imgStream);
            }

            // ------------------------------------------------------------
            // Fill several text fields at once using FillFields.
            // ------------------------------------------------------------
            string[] fieldNames  = { "Address", "City", "PostalCode" };
            string[] fieldValues = { "123 Main St.", "Metropolis", "12345" };
            // The method returns a stream containing the updated PDF; we discard it here.
            form.FillFields(fieldNames, fieldValues, out Stream _);

            // ------------------------------------------------------------
            // Save the updated PDF to the specified output path.
            // ------------------------------------------------------------
            form.Save(outPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outPdfPath}'.");
    }
}