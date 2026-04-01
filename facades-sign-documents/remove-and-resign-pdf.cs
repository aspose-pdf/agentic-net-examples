using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string unsignedPath = "unsigned.pdf";
        const string signedPath = "signed.pdf";
        const string certificatePath = "newcert.pfx";
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

        // Step 1: Remove all existing signatures
        PdfFileSignature remover = new PdfFileSignature();
        remover.BindPdf(inputPath);
        remover.RemoveSignatures();
        remover.Save(unsignedPath);
        remover.Close();

        // Step 2: Sign the PDF with the updated certificate
        PdfFileSignature signer = new PdfFileSignature();
        signer.BindPdf(unsignedPath);
        PKCS1 pkcs1 = new PKCS1(certificatePath, certificatePassword);
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);
        signer.Sign(1, "Updated signature", "contact@example.com", "New York, USA", true, signatureRect, pkcs1);
        signer.Save(signedPath);
        signer.Close();

        Console.WriteLine($"Signature removed and re‑applied. Output saved to {signedPath}");
    }
}