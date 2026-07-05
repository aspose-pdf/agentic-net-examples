using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

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

        // Bind the PDF and extract the signature image
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Extract the image associated with the signature field "WitnessSignature"
            // Use the overload that accepts a string name (SignatureName class has no public ctor).
            Stream jpegStream = pdfSignature.ExtractImage("WitnessSignature");
            if (jpegStream == null)
            {
                Console.Error.WriteLine("Signature image not found.");
                return;
            }

            // Convert JPEG stream to PNG and save it
            using (jpegStream)
            using (var image = Image.FromStream(jpegStream))
            using (var outStream = File.Create(outputPng))
            {
                image.Save(outStream, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Signature image saved to '{outputPng}'.");
    }
}
