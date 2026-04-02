using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        string pdfFile = "output.pdf";

        // ------------------------------------------------------------
        // 1. Create a new PDF document (no XML loading required).
        // ------------------------------------------------------------
        Document pdfDocument = new Document();
        // Add a blank page to the document.
        Page page = pdfDocument.Pages.Add();

        // ------------------------------------------------------------
        // 2. Define the rectangle where the barcode will be placed.
        //    The constructor parameters are (left, bottom, right, top).
        // ------------------------------------------------------------
        Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

        // ------------------------------------------------------------
        // 3. Create a Code128 barcode field, set its value and make it read‑only.
        // ------------------------------------------------------------
        BarcodeField barcodeField = new BarcodeField(page, barcodeRect);
        barcodeField.AddBarcode("1234567890");
        barcodeField.ReadOnly = true;

        // ------------------------------------------------------------
        // 4. Save the resulting PDF.
        // ------------------------------------------------------------
        pdfDocument.Save(pdfFile);
    }
}