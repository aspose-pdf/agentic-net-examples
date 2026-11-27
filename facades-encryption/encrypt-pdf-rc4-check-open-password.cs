using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;
using Aspose.Pdf.Facades; // Added for PdfFileInfo

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "openPwdRC4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve PDF information, including whether it has an open password
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            Console.WriteLine($"HasOpenPassword: {pdfInfo.HasOpenPassword}");
        }

        // Define permissions – using 0 (no restrictions) to allow all actions
        Permissions permissions = (Permissions)0; // equivalent to "allow all"

        // Load the document and encrypt it with RC4x128 algorithm
        Document doc = new Document(inputPath);
        doc.Encrypt(
            userPassword: "u6",
            ownerPassword: "o6",
            permissions: permissions,
            cryptoAlgorithm: CryptoAlgorithm.RC4x128);

        // Save the encrypted PDF
        doc.Save(outputPath);
        Console.WriteLine($"Encrypted PDF saved as '{outputPath}'.");
    }
}
