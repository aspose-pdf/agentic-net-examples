using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity is a Facades class that handles security settings.
        // The constructor receives the source and destination file paths.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // DocumentPrivilege.Print allows printing only; copying is disabled.
            bool result = security.SetPrivilege(DocumentPrivilege.Print);
            if (!result)
            {
                Console.Error.WriteLine("Failed to apply the privilege settings.");
                return;
            }
        }

        Console.WriteLine($"PDF saved with printing allowed and copying disallowed: {outputPath}");
    }
}