using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Use the document title as a unique identifier; fallback to a GUID
            string docId = !string.IsNullOrEmpty(srcDoc.Info.Title)
                ? srcDoc.Info.Title
                : Guid.NewGuid().ToString();

            // Create a temporary PDF that contains the barcode
            using (MemoryStream barcodePdfStream = new MemoryStream())
            {
                using (Document barcodeDoc = new Document())
                {
                    // Add a single page to host the barcode
                    Page barcodePage = barcodeDoc.Pages.Add();

                    // Define the rectangle where the barcode will be placed
                    Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Create a barcode field and set its value to the document identifier
                    BarcodeField barcodeField = new BarcodeField(barcodeDoc, barcodeRect)
                    {
                        Value = docId // Assign the barcode data
                        // The default barcode type is Code128; explicit setting is unnecessary for older SDK versions
                    };

                    // Add the barcode field to the form of the temporary document
                    barcodeDoc.Form.Add(barcodeField);

                    // Save the temporary PDF containing the barcode into the memory stream
                    barcodeDoc.Save(barcodePdfStream);
                }

                // Reset stream position before reading
                barcodePdfStream.Position = 0;

                // Prepare the PdfFileStamp facade for stamping the original PDF
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.BindPdf(inputPath); // Bind the source PDF

                // Create a stamp and bind the barcode PDF page as its content
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(barcodePdfStream, 1); // Use the first (and only) page as stamp
                stamp.IsBackground = false;        // Place stamp on top of page content

                // Position and size of the stamp on each page
                stamp.SetOrigin(150, 400);          // X, Y coordinates (bottom‑left origin)
                stamp.SetImageSize(200, 100);       // Width, Height of the stamp

                // Add the stamp to the document
                fileStamp.AddStamp(stamp);

                // Save the stamped PDF
                fileStamp.Save(outputPath);
                fileStamp.Close();
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
