using System;
using System.IO;
using Aspose.Pdf;                       // Core PDF classes
using Aspose.Pdf.Facades;               // FormEditor and Form facades
using Aspose.Pdf.Facades;               // FieldType enum (same namespace)

class Program
{
    static void Main()
    {
        const string outputTemplatePath = "qr_form_template.pdf";
        const string outputFilledPath   = "qr_form_filled.pdf";
        const string fieldName          = "QRCodeField";
        const string userData           = "https://example.com/user?id=12345";

        // -----------------------------------------------------------------
        // 1. Create a new PDF document with a single blank page.
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add an empty page (default size A4).
            doc.Pages.Add();

            // -----------------------------------------------------------------
            // 2. Add a barcode field (QR Code) to the page using FormEditor.
            // -----------------------------------------------------------------
            // FieldType.Barcode creates a barcode form field.
            // Coordinates are given in points (1/72 inch). Adjust as needed.
            using (FormEditor editor = new FormEditor(doc))
            {
                // page number is 1‑based, rectangle: lower‑left (llx,lly) to upper‑right (urx,ury)
                editor.AddField(FieldType.Barcode, fieldName, 1, 100, 500, 200, 600);
                // Save the intermediate PDF that contains the barcode field.
                editor.Save(outputTemplatePath);
            }
        }

        // -----------------------------------------------------------------
        // 3. Fill the barcode field with user‑entered data (QR code value).
        // -----------------------------------------------------------------
        using (Form form = new Form(outputTemplatePath))
        {
            // FillBarcodeField generates the QR code based on the provided data.
            bool success = form.FillBarcodeField(fieldName, userData);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to fill barcode field '{fieldName}'.");
                return;
            }

            // Save the final PDF with the populated QR code.
            form.Save(outputFilledPath);
        }

        Console.WriteLine($"QR code form created: {outputTemplatePath}");
        Console.WriteLine($"QR code form filled and saved: {outputFilledPath}");
    }
}