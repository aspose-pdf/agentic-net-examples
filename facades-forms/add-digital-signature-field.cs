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
        const string outputPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string tempPath = "temp_with_field.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Step 1: Add a signature field named "DigitalSignature" to the first page.
        using (Document document = new Document(inputPath))
        {
            FormEditor formEditor = new FormEditor(document);
            // Add signature field: page 1, lower‑left x=100, y=100, upper‑right x=200, y=150.
            formEditor.AddField(FieldType.Signature, "DigitalSignature", 1, 100, 100, 200, 150);
            document.Save(tempPath);
        }

        // Step 2: Sign the newly added field.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(tempPath);
            // Optional: set a visual appearance image.
            // pdfSignature.SignatureAppearance = "appearance.png";

            // Use PKCS7 for digital signatures – it provides Reason, ContactInfo, Location, etc.
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
            pkcs7Signature.Reason = "Document approved";
            pkcs7Signature.ContactInfo = "john.doe@example.com"; // corrected property name
            pkcs7Signature.Location = "New York";

            // Sign the field by its name.
            pdfSignature.Sign("DigitalSignature", pkcs7Signature);
            pdfSignature.Save(outputPath);
        }

        // Clean up the temporary file.
        try
        {
            File.Delete(tempPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
