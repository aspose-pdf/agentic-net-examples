using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare folders
        string inputFolder = "input";
        string archiveFolder = "archive";
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(archiveFolder);

        // Create a sample PDF in the input folder
        string inputFilePath = Path.Combine(inputFolder, "sample.pdf");
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(inputFilePath);
        }

        // Encrypt the PDF using PdfFileSecurity
        string encryptedFileName = "encrypted.pdf";
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            fileSecurity.BindPdf(inputFilePath);
            fileSecurity.EncryptFile("user123", "owner123", DocumentPrivilege.Print, KeySize.x256);
            fileSecurity.Save(encryptedFileName);
        }

        // Move the encrypted PDF to the secure archive folder
        string destinationPath = Path.Combine(archiveFolder, encryptedFileName);
        if (File.Exists(destinationPath))
        {
            File.Delete(destinationPath);
        }
        File.Move(encryptedFileName, destinationPath);
    }
}