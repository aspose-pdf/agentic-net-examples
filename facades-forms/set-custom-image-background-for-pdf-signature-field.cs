using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF that contains a signature field named "Signature"
        const string inputPdf = "input.pdf";
        // Output PDF with the updated appearance
        const string outputPdf = "output.pdf";
        // Image to be used as the background of the signature appearance
        const string appearanceImage = "signature_bg.jpg";
        // Path to a certificate (required for signing). Replace with a valid certificate file.
        const string certFile = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(appearanceImage))
        {
            Console.Error.WriteLine($"Appearance image not found: {appearanceImage}");
            return;
        }
        if (!File.Exists(certFile))
        {
            Console.Error.WriteLine($"Certificate file not found: {certFile}");
            return;
        }

        // Initialize the PdfFileSignature facade
        PdfFileSignature pdfSign = new PdfFileSignature();

        // Bind the existing PDF document
        pdfSign.BindPdf(inputPdf);

        // Set the image that will be used as the signature appearance background
        pdfSign.SignatureAppearance = appearanceImage;

        // Create a PKCS#1 signature object (any signature type works)
        PKCS1 signature = new PKCS1(certFile, certPassword);

        // Configure a custom appearance for the signature.
        // The image set via SignatureAppearance will be drawn as a background because
        // IsForegroundImage is set to false.
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            IsForegroundImage = false,               // draw the image as background
            BackgroundColor = Aspose.Pdf.Color.Transparent,
            ForegroundColor = Aspose.Pdf.Color.Black,
            FontFamilyName = "Arial",
            FontSize = 10
        };
        signature.CustomAppearance = customAppearance;

        // Sign the specific field named "Signature" using the prepared signature object.
        // The appearance image set above will be applied to this field.
        pdfSign.Sign("Signature", signature);

        // Save the modified PDF
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signature field appearance updated and saved to '{outputPdf}'.");
    }
}