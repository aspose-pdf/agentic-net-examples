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
        const string password = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfFileInfo (Facades API) to determine if the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        if (!fileInfo.IsEncrypted)
        {
            // If not encrypted, simply copy the file (no decryption needed)
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("PDF was not encrypted; copied without decryption.");
            return;
        }

        // Load the encrypted document with the user password and decrypt it
        using (Document doc = new Document(inputPath, password))
        {
            // Decrypt() takes no parameters
            doc.Decrypt();
            doc.Save(outputPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}