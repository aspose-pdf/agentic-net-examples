using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF (deterministic disposal via using)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the QR code will be placed (coordinates in points)
            Rectangle qrRect = new Rectangle(100, 500, 200, 600);

            // Create a QR code barcode field using the (Page, Rectangle) constructor.
            // Set the field name and the value that will be encoded as a QR code.
            Page firstPage = doc.Pages[1];
            BarcodeField qrField = new BarcodeField(firstPage, qrRect)
            {
                Name = "QrCodeField",
                Value = "https://example.com"
            };

            // Add the barcode field to the document's form collection.
            doc.Form.Add(qrField);

            // Access the low‑level content stream of the page and demonstrate graphics‑state operators.
            OperatorCollection ops = firstPage.Contents;
            ops.Insert(0, new GSave());   // PDF "q" – save graphics state
            ops.Add(new GRestore());      // PDF "Q" – restore graphics state

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"QR code inserted and saved to '{outputPath}'.");
    }
}
