using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to be signed
        const string inputDirectory = "pdfs";

        // Path to the PFX certificate and its password
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Timestamp Authority (TSA) URL
        const string timestampUrl = "http://timestamp.authority/url";

        // Validate inputs
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate not found: {certificatePath}");
            return;
        }

        // Iterate over all PDF files in the directory
        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Create output file name with a suffix
            string signedFilePath = Path.Combine(
                Path.GetDirectoryName(pdfFilePath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(pdfFilePath) + "_signed.pdf");

            try
            {
                // Load the PDF document
                Document doc = new Document(pdfFilePath);

                // Define the rectangle where the visible signature will appear (optional)
                // Adjust coordinates as needed: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

                // Create a signature field and add it to the first page
                SignatureField sigField = new SignatureField(doc, sigRect);
                doc.Form.Add(sigField, 1); // page numbers are 1‑based

                // Prepare the PKCS7 signature object (core API, no Facades)
                PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document approved",
                    ContactInfo = "contact@example.com",
                    Location = "Company"
                };

                // Set timestamp settings (PKCS7.TimestampServerUrl does not exist)
                pkcs7.TimestampSettings = new TimestampSettings(timestampUrl, string.Empty);

                // Sign the field
                sigField.Sign(pkcs7);

                // Save the signed PDF
                doc.Save(signedFilePath);

                Console.WriteLine($"Signed: {signedFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to sign '{pdfFilePath}': {ex.Message}");
            }
        }
    }
}
