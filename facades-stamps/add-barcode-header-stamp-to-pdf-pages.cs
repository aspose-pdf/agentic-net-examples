using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_barcode_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle positioned at the top of the page.
                // Coordinates: lower‑left (llx, lly) and upper‑right (urx, ury).
                // Here we place the barcode 50 points from the left edge,
                // 20 points from the top edge, with a width of 150 points and height of 50 points.
                Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(
                    llx: 50,
                    lly: page.PageInfo.Height - 70,   // top margin = 20
                    urx: 200,
                    ury: page.PageInfo.Height - 20    // bottom of the barcode field
                );

                // Create a BarcodeField on the current page.
                // The constructor does not add the field to the page automatically,
                // so we add it to the page's Annotations collection.
                BarcodeField barcodeField = new BarcodeField(page, barcodeRect);
                page.Annotations.Add(barcodeField);

                // Generate a Code128 barcode that encodes the page number.
                // AddBarcode renders the barcode and makes the field read‑only.
                barcodeField.AddBarcode(i.ToString());

                // Optional: adjust appearance (size, color) if needed.
                // Height and Width are already defined by the rectangle,
                // but you can modify them here:
                // barcodeField.Height = 50;
                // barcodeField.Width  = 150;
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode headers saved to '{outputPath}'.");
    }
}