using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class BatchSigner
{
    static void Main()
    {
        // Input parameters – adjust as needed
        const string inputFolder   = @"C:\PdfInput";          // Folder containing PDFs to sign
        const string outputFolder  = @"C:\PdfSigned";         // Folder where signed PDFs will be saved
        const string pfxPath       = @"C:\cert\mycert.pfx";   // PFX file with signing certificate
        const string pfxPassword   = "pfxPassword";           // Password for the PFX file
        const string tsaUrl        = "https://timestamp.example.com"; // Timestamp Authority URL

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfFile))
            {
                // Create a signature field on the first page (adjust rectangle as needed)
                SignatureField sigField = new SignatureField(
                    doc.Pages[1],
                    new Aspose.Pdf.Rectangle(100, 100, 200, 150));

                // Load the certificate stream (new stream for each document)
                using (FileStream pfxStream = File.OpenRead(pfxPath))
                {
                    // Create a PKCS#7 signature object using the certificate
                    PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword);

                    // Configure timestamp settings (optional but required by the task)
                    pkcs7.TimestampSettings = new TimestampSettings(
                        tsaUrl,               // Timestamp server URL
                        string.Empty,         // No authentication credentials
                        DigestHashAlgorithm.Sha256);

                    // Sign the document using the signature field
                    // The Sign method takes the signature object, the certificate stream, and the password
                    sigField.Sign(pkcs7, pfxStream, pfxPassword);
                }

                // Add the signature field to the document's form fields collection
                // The second argument (1) specifies the page index (1‑based) where the field is placed
                doc.Form.Add(sigField, 1);

                // Build the output file path
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

                // Save the signed PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signed: {Path.GetFileName(pdfFile)} → {outputFolder}");
        }
    }
}
