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
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string appearanceImage = "signature.png";

        if (!File.Exists(inputPath) || !File.Exists(certPath) || !File.Exists(appearanceImage))
        {
            Console.Error.WriteLine("Required file missing.");
            return;
        }

        // Initialize the PDF signature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Load the PDF to be signed
            pdfSign.BindPdf(inputPath);

            // Set the image that will be used as the signature appearance
            pdfSign.SignatureAppearance = appearanceImage;

            // Provide the signing certificate
            pdfSign.SetCertificate(certPath, certPassword);

            // Create a PKCS#1 signature object and set its basic properties
            PKCS1 pkcs1 = new PKCS1(certPath, certPassword);
            pkcs1.Reason = "Approved";
            pkcs1.ContactInfo = "john.doe@example.com";
            pkcs1.Location = "New York";

            // Configure a custom appearance: background color and foreground image flag
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.BackgroundColor = Aspose.Pdf.Color.LightGray;
            customAppearance.IsForegroundImage = true; // draw the image in the foreground
            customAppearance.ForegroundColor = Aspose.Pdf.Color.Blue; // optional text color

            // Assign the custom appearance to the signature object
            pkcs1.CustomAppearance = customAppearance;

            // Define the rectangle where the visible signature will be placed (System.Drawing.Rectangle)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Apply the signature on page 1
            pdfSign.Sign(1, pkcs1.Reason, pkcs1.ContactInfo, pkcs1.Location, true, rect, pkcs1);

            // Save the signed PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}