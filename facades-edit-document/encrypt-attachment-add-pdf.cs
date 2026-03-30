using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPdf = "attachment.pdf"; // PDF to be encrypted and attached
        const string encryptedAttachment = "attachment_encrypted.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPdf))
        {
            Console.Error.WriteLine($"Attachment PDF not found: {attachmentPdf}");
            return;
        }

        // Encrypt the attachment PDF using PdfFileSecurity
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            fileSecurity.BindPdf(attachmentPdf);
            // Encrypt with 256‑bit AES, allow printing privilege
            // The overload does not accept an "algorithm" argument – the algorithm is inferred from the key size.
            fileSecurity.EncryptFile(
                userPassword: "user123",
                ownerPassword: "owner123",
                privilege: DocumentPrivilege.Print,
                keySize: KeySize.x256);
            fileSecurity.Save(encryptedAttachment);
        }

        // Add the encrypted attachment to the main PDF as an embedded file (portfolio)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a FileSpecification for the encrypted attachment
            var fileSpec = new FileSpecification(Path.GetFileName(encryptedAttachment));
            fileSpec.Description = "Encrypted attachment";
            // Load the encrypted file into a memory stream and assign it to the specification
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(encryptedAttachment));

            // The EmbeddedFiles collection is read‑only but already instantiated, so we can add directly.
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Encrypted attachment added and PDF saved as '{outputPdf}'.");
    }
}
