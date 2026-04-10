using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string folderPath = "secure";
        const string userPassword = "MySecretPassword";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        foreach (string inputFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            string outputFile = Path.Combine(
                folderPath,
                Path.GetFileNameWithoutExtension(inputFile) + "_protected.pdf");

            using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
            {
                fileSecurity.BindPdf(inputFile);
                fileSecurity.EncryptFile(
                    userPassword,          // user password
                    null,                  // owner password (null => random)
                    DocumentPrivilege.Print, // privilege (adjust as needed)
                    KeySize.x256);         // strong encryption
                fileSecurity.Save(outputFile);
            }

            Console.WriteLine($"Encrypted: {outputFile}");
        }
    }
}