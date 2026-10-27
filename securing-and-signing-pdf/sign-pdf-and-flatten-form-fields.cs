using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_flattened.pdf";
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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Define the rectangle for the visible signature (left, bottom, right, top)
                var sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

                // Create a signature field. Providing a rectangle makes the field visible by default.
                var sigField = new SignatureField(doc, sigRect);

                // Add the signature field to the document's AcroForm collection
                doc.Form.Add(sigField);

                // Prepare a PKCS#7 signature (Aspose uses PKCS7 for PDF signatures)
                var pkcs7 = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document approval",
                    Location = "Office",
                    ContactInfo = "John Doe"
                };

                // Sign the field using the PKCS#7 signature
                sigField.Sign(pkcs7);

                // Flatten all form fields so they become static content after signing
                doc.Form.Flatten();

                // Save the signed and flattened PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signed and flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
