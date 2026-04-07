using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF.
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        // Verify that the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file into a PDF document using XmlLoadOptions.
        // The XML defines the layout of the PDF (including form fields).
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Use the Form class from Aspose.Pdf.Facades to work with AcroForm fields.
            Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfDoc);

            // Example: fill a barcode field named "BarcodeField1" with data.
            // The field must exist in the PDF (created by the XML layout).
            // The FillBarcodeField method generates a Code128 barcode.
            bool filled = form.FillBarcodeField("BarcodeField1", "1234567890");
            if (!filled)
            {
                Console.Error.WriteLine("Failed to fill barcode field 'BarcodeField1'.");
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with barcode saved to '{outputPdf}'.");
    }
}