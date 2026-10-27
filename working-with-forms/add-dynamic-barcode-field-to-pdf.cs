using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";   // PDF containing a BarcodeField named "barcode"
        const string outputPath   = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Generate a unique value for the barcode (e.g., 12‑character alphanumeric string)
        string uniqueBarcodeValue = Guid.NewGuid().ToString("N").Substring(0, 12);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(templatePath))
        {
            // Retrieve the barcode form field by its name
            if (doc.Form["barcode"] is BarcodeField barcodeField)
            {
                // The barcode type (e.g., Code128) is usually defined in the template.
                // If you need to change it programmatically, ensure you are using a version of Aspose.Pdf that exposes the BarcodeType property.
                // For compatibility with older versions we only set the value here.
                barcodeField.Value = uniqueBarcodeValue;

                // Visual appearance (color, background) is controlled by the field's appearance settings in the template.
                // The older Aspose.Pdf versions do not expose TextColor or BackgroundColor on BarcodeField, so we omit them.
            }
            else
            {
                Console.Error.WriteLine("Barcode field named 'barcode' not found in the PDF.");
                return;
            }

            // Save the modified PDF (saving as PDF does not require SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Generated barcode '{uniqueBarcodeValue}' and saved to '{outputPath}'.");
    }
}
