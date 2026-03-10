using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";   // Path to the encrypted PDF
        const string password   = "owner123";      // Owner or user password

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the PdfFileSecurity facade (Aspose.Pdf.Facades) to open and decrypt the PDF
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Load the encrypted PDF file into the facade
            security.BindPdf(inputPath);

            // Decrypt the document using the supplied password.
            // DecryptFile throws if decryption fails.
            security.DecryptFile(password);

            // Retrieve the decrypted Document object from the facade.
            using (Document doc = security.Document)
            {
                // Example: save the decrypted PDF into a memory stream (in‑memory representation)
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);
                    Console.WriteLine($"Decrypted PDF loaded into memory, size = {ms.Length} bytes.");
                    // The MemoryStream now contains the PDF bytes and can be used further.
                }
            }
        }
    }
}