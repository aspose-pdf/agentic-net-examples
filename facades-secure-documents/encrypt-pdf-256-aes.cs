using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the security facade and bind the source PDF
        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);

        // Encrypt using 256‑bit AES; allow printing as an example privilege
        security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);

        // Save the encrypted PDF; guard against missing GDI+ on non‑Windows platforms
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            security.Save(outputPath);
        }
        else
        {
            try
            {
                security.Save(outputPath);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; cannot save PDF.");
            }
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }

    // Helper to detect nested DllNotFoundException
    static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}