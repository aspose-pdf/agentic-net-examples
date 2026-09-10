using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF to obtain its unique identifier (using the document title as an example)
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Use the document title; fallback to a GUID if title is empty
            string uniqueId = !string.IsNullOrEmpty(srcDoc.Info.Title)
                              ? srcDoc.Info.Title
                              : Guid.NewGuid().ToString();

            // ------------------------------------------------------------
            // Create a temporary PDF that contains only the barcode field
            // ------------------------------------------------------------
            using (MemoryStream barcodePdfStream = new MemoryStream())
            {
                using (Document barcodeDoc = new Document())
                {
                    // Add a single blank page
                    Page barcodePage = barcodeDoc.Pages.Add();

                    // Define the barcode rectangle (position and size)
                    // Rectangle(left, bottom, width, height)
                    Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 200, 100);

                    // Create the barcode field on the page
                    BarcodeField barcodeField = new BarcodeField(barcodePage, barcodeRect);
                    // Set the barcode value to the unique identifier
                    barcodeField.Value = uniqueId;
                    // Optionally set the barcode type (Code128 is default)
                    // barcodeField.BarcodeType = BarcodeType.Code128;

                    // Add the barcode field to the page's annotations collection
                    barcodePage.Annotations.Add(barcodeField);

                    // Save the temporary PDF containing the barcode to a memory stream
                    barcodeDoc.Save(barcodePdfStream);
                }

                // Reset stream position for reading
                barcodePdfStream.Position = 0;

                // ------------------------------------------------------------
                // Aspose.Pdf.Facades.Stamp the original PDF with the barcode page
                // ------------------------------------------------------------
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.InputFile  = inputPdfPath;
                fileStamp.OutputFile = outputPdfPath;

                // Create a stamp that uses the first page of the barcode PDF
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(barcodePdfStream, 1); // use page 1 as stamp content
                stamp.IsBackground = false;        // place stamp on top of page content

                // Add the stamp to the document and finalize
                fileStamp.AddStamp(stamp);
                fileStamp.Close(); // writes the output file
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}