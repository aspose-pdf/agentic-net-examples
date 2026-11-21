using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for source PDF, signed output, and signing certificate
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Validate required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create a signature field (invisible) and add it to the form collection
        // Rectangle with zero size makes the field invisible; you can also omit the rectangle and set Visible = false later.
        var sigField = new SignatureField(doc, new Aspose.Pdf.Rectangle(0, 0, 0, 0))
        {
            Name = "Signature1"
        };
        doc.Form.Add(sigField);

        // Create a PKCS7 signature object with empty Reason, Location and ContactInfo to suppress those texts
        var pkcs7 = new PKCS7(certPath, certPassword)
        {
            Reason = string.Empty,
            Location = string.Empty,
            ContactInfo = string.Empty,
            // Optional: hide all labels via custom appearance (not required for an invisible field)
            CustomAppearance = new SignatureCustomAppearance
            {
                ShowReason = false,
                ShowLocation = false,
                ShowContactInfo = false
            }
        };

        // Sign the field with the PKCS7 object
        sigField.Sign(pkcs7);

        // Save the signed document
        doc.Save(outputPath);

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
