using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "encrypted.pdf";
        const string ownerPassword = "ownerSecret";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Encrypt the document: no user password, only owner password,
        // and forbid all permissions (no flags set)
        doc.Encrypt(
            userPassword: null,
            ownerPassword: ownerPassword,
            permissions: (Permissions)0,
            cryptoAlgorithm: CryptoAlgorithm.AESx256);

        // Save the encrypted PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Encrypted PDF saved to '{outputPdf}'.");
    }
}
