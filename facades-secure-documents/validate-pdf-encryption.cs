using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string password = "ownerpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine whether the PDF is encrypted without opening it fully.
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        if (!fileInfo.IsEncrypted)
        {
            Console.WriteLine("PDF is not encrypted – copying original file.");
            using (Document doc = new Document(inputPath))
            {
                doc.Save(outputPath);
            }
            return;
        }

        // PDF is encrypted – open with the password and decrypt.
        using (Document encryptedDoc = new Document(inputPath, password))
        {
            encryptedDoc.Decrypt();
            encryptedDoc.Save(outputPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}
