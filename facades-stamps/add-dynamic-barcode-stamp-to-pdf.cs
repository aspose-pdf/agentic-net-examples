using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // BarcodeField and BarcodeType are in this namespace

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

        // Load the source PDF to obtain a unique identifier (use Title if present, otherwise a GUID)
        string uniqueId;
        using (Document srcDoc = new Document(inputPath))
        {
            uniqueId = !string.IsNullOrEmpty(srcDoc.Info.Title) ? srcDoc.Info.Title : Guid.NewGuid().ToString();
        }

        // Create a temporary PDF that contains the barcode
        using (Document stampDoc = new Document())
        {
            // Add a page for the barcode
            Page barcodePage = stampDoc.Pages.Add();

            // Define the barcode rectangle (left, bottom, width, height)
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(0, 0, 200, 100);

            // Create a BarcodeField on the temporary page
            BarcodeField barcodeField = new BarcodeField(stampDoc, barcodeRect);
            // Optional: set explicit barcode type
            // barcodeField.BarcodeType = BarcodeType.Code128;
            // Set the barcode value – use the Value property (not BarcodeValue)
            barcodeField.Value = uniqueId;

            // Add the barcode field to the page
            barcodePage.Paragraphs.Add(barcodeField);

            // Save the temporary PDF to a memory stream
            using (MemoryStream stampStream = new MemoryStream())
            {
                stampDoc.Save(stampStream);
                stampStream.Position = 0;

                // Prepare the PdfFileStamp facade
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.BindPdf(inputPath);

                // Create a stamp that uses the first page of the temporary PDF as its content
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(stampStream, 1); // bind page 1 of the temporary PDF
                stamp.IsBackground = false;   // place stamp on top of page content
                stamp.SetOrigin(100, 100);    // position of the stamp (X, Y)
                stamp.SetImageSize(200, 100); // size of the stamp (width, height)

                // Add the stamp to the document and save
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPath);
                fileStamp.Close(); // releases resources
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
