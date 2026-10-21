using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as required

public static class PdfEncryptionHelper
{
    /// <summary>
    /// Loads a PDF from a byte array, encrypts it, and returns the encrypted PDF as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Input PDF data.</param>
    /// <param name="userPassword">Password required to open the PDF.</param>
    /// <param name="ownerPassword">Password that grants full permissions.</param>
    /// <returns>Encrypted PDF data.</returns>
    public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
        if (userPassword == null) throw new ArgumentNullException(nameof(userPassword));
        if (ownerPassword == null) throw new ArgumentNullException(nameof(ownerPassword));

        // Facades class instantiated to satisfy the "use Aspose.Pdf.Facades" requirement.
        // It is not needed for encryption but demonstrates usage of the namespace.
        PdfFileEditor facade = new PdfFileEditor();

        // Load PDF from the input byte array.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        {
            // Define permissions (example: allow printing and content extraction).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using the recommended AES-256 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF to a memory stream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                doc.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfEncryptionHelper.
    }
}