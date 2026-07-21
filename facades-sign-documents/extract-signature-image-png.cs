using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf;                 // for any PDF related types (kept for completeness)
using System.Drawing;            // Image handling (Windows only)
using System.Drawing.Imaging;    // ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "WitnessSignature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the PdfFileSignature facade
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Extract the image stream for the signature field named "WitnessSignature"
            // The ExtractImage method accepts the field name directly (string overload).
            Stream jpegStream = pdfSignature.ExtractImage("WitnessSignature");
            if (jpegStream == null)
            {
                Console.Error.WriteLine("Signature image not found.");
                return;
            }

            // Convert the JPEG stream to PNG and save it
            using (jpegStream)
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(jpegStream))
            {
                img.Save(outputPng, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Signature image saved to '{outputPng}'.");
    }
}
