using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor does not expose the needed API).
        Document pdfDoc = new Document(inputPath);

        // Define the permissions using only members that exist in the current Aspose.Pdf version.
        Permissions permissions = Permissions.PrintDocument |
                                   Permissions.ExtractContent |
                                   Permissions.ModifyContent;

        // Apply the permissions. Empty passwords keep the original passwords (if any).
        // Choose any supported CryptoAlgorithm; RC4x128 is used here as an example.
        pdfDoc.Encrypt(string.Empty, string.Empty, permissions, CryptoAlgorithm.RC4x128);

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Privileges modified and saved to '{outputPath}'.");
    }
}
