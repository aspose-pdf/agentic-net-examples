using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Form field types, including BarcodeField

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Generate a unique value for the barcode (e.g., a GUID)
        string barcodeData = Guid.NewGuid().ToString();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(templatePath))
        {
            // Retrieve the barcode form field by its name
            // The field must exist in the PDF and be of type BarcodeField
            BarcodeField barcodeField = doc.Form["barcodeField"] as BarcodeField;

            if (barcodeField != null)
            {
                // Assign the dynamic data to the field
                barcodeField.Value = barcodeData;

                // NOTE: In the version of Aspose.Pdf bundled with this project the
                // BarcodeField does not expose a "BarcodeType" property. The default
                // symbology (Code128) is sufficient for most scenarios. If a specific
                // symbology is required, upgrade to a newer Aspose.Pdf version that
                // provides the BarcodeType property.
            }
            else
            {
                Console.Error.WriteLine("Barcode field 'barcodeField' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with barcode data: {barcodeData}");
    }
}
