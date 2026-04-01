using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle
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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Create a PKCS7 signature object and suppress textual fields
            PKCS7 signature = new PKCS7(certPath, certPassword);
            signature.Reason = string.Empty;        // suppress reason text
            signature.ContactInfo = string.Empty;   // suppress contact text
            signature.Location = string.Empty;      // suppress location text

            // Define the rectangle where the visible signature will be placed.
            // System.Drawing.Rectangle expects (x, y, width, height).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 100, 50);

            // Sign the first page, make the signature visible.
            pdfSign.Sign(1, true, rect, signature);
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
