using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputDirectory = "input";
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory '{inputDirectory}' does not exist.");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to encrypt.");
            return;
        }

        foreach (string pdfFilePath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfFilePath);
            string outputFileName = fileNameWithoutExt + "_enc.pdf";

            Stopwatch timer = new Stopwatch();
            timer.Start();

            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(pdfFilePath);
            // Encrypt with user password, owner password, Print privilege, 256‑bit AES
            fileSecurity.EncryptFile("userpass", "ownerpass", DocumentPrivilege.Print, KeySize.x256);
            fileSecurity.Save(outputFileName);

            timer.Stop();
            Console.WriteLine($"Encrypted '{outputFileName}' in {timer.ElapsedMilliseconds} ms.");
        }
    }
}