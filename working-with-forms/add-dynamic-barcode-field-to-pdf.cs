using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "output_with_barcode.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Define the rectangle where the barcode field will be placed
            // Parameters: left, bottom, width, height
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a barcode field on the first page
            BarcodeField barcodeField = new BarcodeField(doc.Pages[1], rect);

            // Generate a unique value for the barcode (e.g., a GUID without hyphens)
            string uniqueValue = Guid.NewGuid().ToString("N");

            // Populate the field with the barcode (Code128 is the default symbology)
            barcodeField.AddBarcode(uniqueValue);

            // Optionally assign a name to the field
            barcodeField.Name = "UniqueBarcode";

            // Add the barcode field to the page's annotation collection
            doc.Pages[1].Annotations.Add(barcodeField);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode field added and saved to '{outputPath}'.");
    }
}