using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string[] inputFiles = new string[] { "file1.pdf", "file2.pdf" };
        string userPassword = "user123";
        string ownerPassword = "owner123";
        DocumentPrivilege privilege = DocumentPrivilege.Print;
        KeySize keySize = KeySize.x256;

        foreach (string inputFile in inputFiles)
        {
            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"Input file not found: {inputFile}");
                continue;
            }

            string outputFile = Path.GetFileNameWithoutExtension(inputFile) + "_encrypted.pdf";

            Stopwatch timer = new Stopwatch();
            timer.Start();

            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputFile, outputFile);
            bool success = fileSecurity.TryEncryptFile(userPassword, ownerPassword, privilege, keySize);

            timer.Stop();

            Console.WriteLine($"Encrypted '{inputFile}' to '{outputFile}' in {timer.ElapsedMilliseconds} ms. Success: {success}");
        }
    }
}