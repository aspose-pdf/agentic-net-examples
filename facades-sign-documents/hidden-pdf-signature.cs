using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            pdfSignature.SetCertificate(certificatePath, certificatePassword);
            Rectangle signatureRect = new Rectangle(0, 0, 0, 0);
            pdfSignature.Sign(1, "Document signed", "Signer", "Location", false, signatureRect);
            pdfSignature.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved as '{outputPath}' with hidden appearance.");
    }
}