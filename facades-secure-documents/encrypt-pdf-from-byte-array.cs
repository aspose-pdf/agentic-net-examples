using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Encrypts a PDF supplied as a byte array and returns the encrypted PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Original PDF content.</param>
    /// <param name="userPassword">Password required for opening the PDF.</param>
    /// <param name="ownerPassword">Owner password that controls permissions.</param>
    /// <returns>Encrypted PDF bytes.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        // Validate input
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

        // Load the PDF from the input byte array into a Document instance
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        // Initialize PdfFileSecurity facade with the loaded document
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(doc))
        // Prepare an output stream to receive the encrypted PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Choose desired privileges (example: allow printing)
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Encrypt the document using 256‑bit AES encryption
            fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x256);

            // Save the encrypted document to the output stream
            fileSecurity.Save(outputStream);

            // Return the encrypted PDF as a byte array
            return outputStream.ToArray();
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (optional). In production this method can be left empty.
        // byte[] originalPdf = File.ReadAllBytes("sample.pdf");
        // byte[] encryptedPdf = PdfEncryptionHelper.EncryptPdf(originalPdf, "user", "owner");
        // File.WriteAllBytes("encrypted.pdf", encryptedPdf);
    }
}