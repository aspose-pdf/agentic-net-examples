using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "ownerPwd";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);
        try
        {
            bool success = fileSecurity.DecryptFile(ownerPassword);
            if (success)
            {
                Console.WriteLine($"File decrypted successfully to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Decryption failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during decryption: {ex.Message}");
        }
        finally
        {
            fileSecurity.Close();
        }
    }
}
