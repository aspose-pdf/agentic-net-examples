using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain a unique identifier (use Title if present, otherwise file name)
        string docId;
        using (Document tempDoc = new Document(inputPath))
        {
            docId = !string.IsNullOrEmpty(tempDoc.Info.Title)
                ? tempDoc.Info.Title
                : Path.GetFileNameWithoutExtension(inputPath);
        }

        // Load the PDF document and add a barcode field to each page
        using (Document pdfDoc = new Document(inputPath))
        {
            int pageNumber = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                // Define the rectangle where the barcode will be placed (top‑center of the page)
                // Coordinates: lower‑left (x1, y1), upper‑right (x2, y2)
                // Here we place a 150pt wide by 50pt high field 10pt below the top edge.
                float pageWidth = (float)page.PageInfo.Width;               // cast double -> float
                float fieldWidth = 150f;
                float fieldHeight = 50f;
                float x1 = (pageWidth - fieldWidth) / 2f;                    // centre horizontally
                float y2 = (float)page.PageInfo.Height - 10f;                // 10pt from top, cast double -> float
                float y1 = y2 - fieldHeight;                                 // bottom of the field
                float x2 = x1 + fieldWidth;

                var rect = new Aspose.Pdf.Rectangle(x1, y1, x2, y2);
                var barcodeField = new TextBoxField(page, rect)
                {
                    PartialName = $"barcode_page_{pageNumber}"
                };

                // Add the field to the document's form collection (page index is 1‑based)
                pdfDoc.Form.Add(barcodeField, pageNumber);

                // Draw the barcode into the field. The AddBarcode method supports many symbologies;
                // by default it uses Code128 when the string contains only ASCII characters.
                barcodeField.AddBarcode(docId);

                pageNumber++;
            }

            // Save the stamped PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
