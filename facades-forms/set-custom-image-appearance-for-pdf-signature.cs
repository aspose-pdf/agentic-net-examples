using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Rectangle type for potential future use

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string appearanceImage = "signature_bg.jpg";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(appearanceImage))
        {
            Console.Error.WriteLine($"Signature appearance image not found: {appearanceImage}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the source PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Set a custom appearance for the signature field using an image file
        pdfSign.SignatureAppearance = appearanceImage;

        // If a digital signature is required, set the certificate here:
        // pdfSign.SetCertificate("certificate.pfx", "password");
        // Then sign the field (example):
        // pdfSign.Sign("Signature", new PKCS1("certificate.pfx", "password"));

        // Save the modified PDF with the new signature appearance
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signature field appearance updated and saved to '{outputPdf}'.");
    }
}