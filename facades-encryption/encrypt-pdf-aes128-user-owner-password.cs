using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Needed for PdfFileInfo

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "editPwdAES.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve PDF file information, including edit‑password status
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        Console.WriteLine($"Has edit password: {fileInfo.HasEditPassword}");
        fileInfo.Close(); // Release file handle

        // Load the PDF document (core API)
        Document pdfDoc = new Document(inputPath);

        // Define permissions for the encrypted document.
        Permissions permissions = Permissions.PrintDocument |
                                   Permissions.ModifyContent |
                                   Permissions.ExtractContent |
                                   Permissions.ModifyTextAnnotations |
                                   Permissions.FillForm |
                                   Permissions.ExtractContentWithDisabilities |
                                   Permissions.AssembleDocument |
                                   Permissions.PrintingQuality;

        // Encrypt the PDF using AES‑128 with the specified user and owner passwords
        pdfDoc.Encrypt("u5", "o5", permissions, CryptoAlgorithm.AESx128);

        // Save the encrypted PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved as '{outputPath}'.");
    }
}
