using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Retrieves the certificate stream and password for a given document type.
    // Replace the stub with real DB access logic as needed.
    static (Stream CertStream, string Password) GetCertificateInfo(string documentType)
    {
        // Example implementation – loads a PFX file from a folder relative to the executable.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string certPath = Path.Combine(baseDir, "certs", $"{documentType}.pfx");
        if (!File.Exists(certPath))
            throw new FileNotFoundException($"Certificate file not found: {certPath}");

        // In a real scenario the password would also be fetched from a secure store.
        string password = "pfxPassword";
        Stream stream = File.OpenRead(certPath);
        return (stream, password);
    }

    static void Main()
    {
        // Build input / output folders relative to the executable so the sample works out‑of‑the‑box.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "Input");
        string outputFolder = Path.Combine(baseDir, "Signed");

        // Ensure the folders exist – creates them if they are missing.
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Place PDFs there and rerun the program.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_signed.pdf");

            try
            {
                // Load the PDF document.
                using (Document doc = new Document(inputPath))
                {
                    // Use the document title as a simple way to obtain a document type.
                    // Adjust this logic to match your actual metadata source.
                    string documentType = doc.Info.Title ?? "default";

                    // Iterate over all form fields and sign only the signature fields.
                    foreach (Field field in doc.Form.Fields)
                    {
                        if (field is SignatureField sigField)
                        {
                            var (certStream, password) = GetCertificateInfo(documentType);
                            // Ensure the certificate stream is disposed after signing.
                            using (certStream)
                            {
                                PKCS7 pkcs7Signature = new PKCS7(certStream, password);
                                sigField.Sign(pkcs7Signature);
                            }
                        }
                    }

                    // Save the signed PDF.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Signed PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
