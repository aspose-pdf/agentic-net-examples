using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputDirectory = "input";
        string archiveDirectory = "archive";

        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(archiveDirectory);

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfFilePath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfFilePath);
            string encryptedFilePath = Path.Combine(archiveDirectory, fileName + "_enc.pdf");

            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(pdfFilePath);
            fileSecurity.EncryptFile("user123", "owner123", DocumentPrivilege.Print, KeySize.x256);
            fileSecurity.Save(encryptedFilePath);
            fileSecurity.Close();

            File.Delete(pdfFilePath);
            Console.WriteLine("Encrypted and archived: " + encryptedFilePath);
        }

        Console.WriteLine("Processing complete.");
    }
}