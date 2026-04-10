using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process (absolute path). Adjust as needed.
        const string inputFolderRelative  = @"C:\Pdf\Input";
        const string outputFolderRelative = @"C:\Pdf\Output";

        // Resolve to absolute paths – this works on Windows and non‑Windows platforms.
        string inputFolder  = Path.GetFullPath(inputFolderRelative);
        string outputFolder = Path.GetFullPath(outputFolderRelative);

        // Certificate used for digital signatures
        const string certPath     = @"C:\Certificates\mycert.pfx";
        const string certPassword = "certPassword";

        // Encryption passwords
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure input folder exists before trying to enumerate files.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Encrypt the document (AES‑256)
                    doc.Encrypt(userPassword, ownerPassword,
                                Permissions.PrintDocument | Permissions.ExtractContent,
                                CryptoAlgorithm.AESx256);

                    // Add a visible signature field on the first page
                    Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
                    SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
                    {
                        PartialName = "Signature1"
                    };
                    doc.Form.Add(sigField, 1);

                    // Prepare the PKCS#1 signature object
                    PKCS1 pkcs1 = new PKCS1(certPath, certPassword)
                    {
                        Reason      = "Document approved",
                        ContactInfo = "contact@example.com",
                        Location    = "New York, USA"
                    };

                    // Apply the digital signature to the field
                    sigField.Sign(pkcs1);

                    // Save the encrypted and signed PDF
                    string fileName   = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_signed.pdf");
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption and signing completed.");
    }
}
