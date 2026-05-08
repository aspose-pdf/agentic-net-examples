using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_barcode.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Choose the page where the barcode will be placed
            Page page = doc.Pages[1]; // 1‑based indexing (rule: page-indexing-one-based)

            // Define the rectangle for the barcode field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the selected page (constructor rule)
            BarcodeField barcodeField = new BarcodeField(page, rect);

            // Optionally set a name for the field (useful for later reference)
            barcodeField.Name = "DynamicBarcode";

            // Generate dynamic data (e.g., a GUID‑based code)
            string dynamicCode = Guid.NewGuid().ToString("N").Substring(0, 12); // 12‑character Code128 value

            // Populate the field with the barcode (method from TextBoxField)
            barcodeField.AddBarcode(dynamicCode); // adds Code128 barcode and makes the field read‑only

            // Add the field to the page's annotation collection
            page.Annotations.Add(barcodeField);

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Barcode field added and saved to '{outputPath}'.");
    }
}