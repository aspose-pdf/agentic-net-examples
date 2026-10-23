using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certificatePath = "mycert.pfx";
        const string certificatePassword = "password";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Prepare custom appearance – hide the default "Digitally signed by" caption
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            // Setting an empty label removes the default caption
            DigitalSignedLabel = string.Empty
        };

        // Create a signature field with the desired rectangle
        Rectangle signatureRect = new Rectangle(100, 100, 250, 150);
        SignatureField sigField = new SignatureField(doc, signatureRect)
        {
            PartialName = "Signature1"
        };

        // Assign the custom appearance to the signature field via its Signature property
        sigField.Signature.CustomAppearance = customAppearance;

        // Add the signature field to the document's form collection
        doc.Form.Add(sigField);

        // Create PKCS7 object using the certificate file and assign the same custom appearance
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
        {
            // Optional: you can also set Reason, Location, ContactInfo here if you want them stored
            // Reason = "Approval",
            // Location = "New York",
            // ContactInfo = "signer@example.com",
            CustomAppearance = customAppearance
        };

        // Sign the field
        sigField.Sign(pkcs7);

        // Save the signed PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
