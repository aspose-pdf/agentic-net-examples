using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_header_barcode.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF.
        using (Document srcDoc = new Document(inputPdf))
        {
            int pageCount = srcDoc.Pages.Count;

            // Header barcode dimensions (points).
            const double barcodeWidth  = 150; // width of the barcode image
            const double barcodeHeight = 50;  // height of the barcode image
            const double leftMargin    = 50;  // distance from the left edge
            const double topMargin     = 50;  // distance from the top edge

            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Calculate rectangle for the barcode field.
                // Aspose.Pdf.Rectangle expects (llx, lly, urx, ury).
                double pageHeight = srcDoc.Pages[pageNum].PageInfo.Height;
                double llx = leftMargin;
                double lly = pageHeight - topMargin - barcodeHeight; // lower‑left Y
                double urx = leftMargin + barcodeWidth;
                double ury = pageHeight - topMargin;                // upper‑right Y

                var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a TextBoxField that will hold the barcode.
                var barcodeField = new TextBoxField(srcDoc.Pages[pageNum], rect)
                {
                    PartialName = $"barcode_{pageNum}"
                };

                // Add the field to the document's form collection.
                srcDoc.Form.Add(barcodeField, pageNum);

                // Generate the barcode directly into the field.
                barcodeField.AddBarcode($"PAGE-{pageNum}");
            }

            // Save the modified PDF.
            srcDoc.Save(outputPdf);
        }

        Console.WriteLine($"Header barcode stamp applied. Output saved to '{outputPdf}'.");
    }
}
