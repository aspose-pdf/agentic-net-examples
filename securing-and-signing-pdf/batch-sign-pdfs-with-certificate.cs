using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSigner
{
    static void Main()
    {
        // Input parameters
        const string inputDirectory = @"C:\PdfToSign";          // Folder containing PDFs
        const string certificatePath = @"C:\certs\mycert.pfx";   // PFX file with private key
        const string certificatePassword = "pfxPassword";       // Password for the PFX
        const string timestampServerUrl = "https://timestamp.example.com"; // TSA URL

        // Verify input folder exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Process each PDF file in the directory (non‑recursive)
        foreach (string pdfFile in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(pdfFile))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                    {
                        Console.WriteLine($"Skipping empty document: {pdfFile}");
                        continue;
                    }

                    // Choose the first page for the signature field
                    Page page = doc.Pages[1];

                    // Define a rectangle for the signature appearance (bottom‑right corner)
                    // Use Aspose.Pdf.Rectangle (expects double values) to avoid type‑mismatch with Aspose.Pdf.Drawing.Rectangle
                    Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(
                        page.PageInfo.Width - 150,   // llx
                        20,                          // lly
                        page.PageInfo.Width - 20,    // urx
                        70);                         // ury

                    // Create a signature field and add it to the page annotations
                    SignatureField signatureField = new SignatureField(page, sigRect);
                    page.Annotations.Add(signatureField);

                    // Create a PKCS#7 signature using the certificate file
                    PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
                    {
                        Reason = "Batch signing of documents",
                        Location = "Company HQ",
                        ContactInfo = "signer@example.com",
                        ShowProperties = true
                    };

                    // Configure timestamp settings (optional but required by the task)
                    pkcs7.TimestampSettings = new TimestampSettings(
                        timestampServerUrl,
                        string.Empty,                     // No basic auth credentials
                        DigestHashAlgorithm.Sha256);      // Correct enum value (PascalCase)

                    // Sign the document using the signature field
                    signatureField.Sign(pkcs7);

                    // Overwrite the original file with the signed version
                    doc.Save(pdfFile);
                }

                Console.WriteLine($"Signed: {System.IO.Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error signing '{pdfFile}': {ex.Message}");
            }
        }
    }
}
