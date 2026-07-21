using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for DigestHashAlgorithm

class BatchSigner
{
    static void Main()
    {
        // Input directory containing PDFs to be signed
        const string inputDirectory = @"C:\PdfBatch\Input";
        // Output directory for signed PDFs
        const string outputDirectory = @"C:\PdfBatch\Signed";

        // Certificate (PFX) file and its password
        const string certificatePath = @"C:\Certificates\mycert.pfx";
        const string certificatePassword = "pfxPassword";

        // Timestamp Authority (TSA) settings
        const string tsaServerUrl = "https://timestamp.example.com";
        const string tsaCredentials = ""; // format "username:password" if required, otherwise empty

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Determine output file path
            string outputFilePath = Path.Combine(outputDirectory, Path.GetFileName(pdfFilePath));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfFilePath))
            {
                // Ensure the document has at least one page
                if (pdfDocument.Pages.Count == 0)
                {
                    Console.WriteLine($"Skipping empty document: {pdfFilePath}");
                    continue;
                }

                // Define a rectangle where the signature appearance will be placed (coordinates are in points)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

                // Create a signature field on the first page
                Page firstPage = pdfDocument.Pages[1];
                SignatureField signatureField = new SignatureField(firstPage, signatureRect)
                {
                    // Optional: give the field a unique name
                    Name = "Signature1"
                };

                // Create a PKCS#7 signature object using the certificate file
                PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);

                // Configure timestamp settings (optional but required by the task)
                pkcs7Signature.TimestampSettings = new TimestampSettings(
                    tsaServerUrl,          // TSA server URL
                    tsaCredentials,        // Basic auth credentials (empty if not needed)
                    DigestHashAlgorithm.Sha256); // Correct enum value (PascalCase)

                // Sign the document using the signature field
                signatureField.Sign(pkcs7Signature);

                // Save the signed PDF to the output directory
                pdfDocument.Save(outputFilePath);
            }

            Console.WriteLine($"Signed PDF saved: {outputFilePath}");
        }

        Console.WriteLine("Batch signing completed.");
    }
}
