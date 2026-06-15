using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSecurity, DocumentPrivilege, KeySize, Algorithm

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege object that allows printing and editing (modify contents)
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowPrint          = true;   // enable printing
        privilege.AllowModifyContents = true;   // enable editing of page contents

        // Encrypt the PDF using RC4 with a 128‑bit key
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            bool success = security.EncryptFile(
                userPassword,
                ownerPassword,
                privilege,
                KeySize.x128,      // 128‑bit key size
                Algorithm.RC4);    // RC4 algorithm

            if (!success)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        Console.WriteLine($"PDF encrypted successfully: {outputPath}");
    }
}