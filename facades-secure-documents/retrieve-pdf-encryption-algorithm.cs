using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "protected.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo provides file‑level information without loading the whole document.
        PdfFileInfo fileInfo = new PdfFileInfo(pdfPath);

        if (!fileInfo.IsEncrypted)
        {
            Console.WriteLine("The PDF is not encrypted.");
            return;
        }

        // The EncryptionAlgorithm property is not available in the current Aspose.PDF version.
        // Use a placeholder to indicate that the document is encrypted.
        string algorithmName = "Encrypted (algorithm unknown)";

        // If you upgrade to a version that exposes EncryptionAlgorithm, you can replace the
        // placeholder with the actual value, e.g.:
        // var algo = fileInfo.EncryptionAlgorithm; // CryptoAlgorithm enum
        // algorithmName = algo.ToString();

        Console.WriteLine($"Current encryption algorithm: {algorithmName}");
    }
}
