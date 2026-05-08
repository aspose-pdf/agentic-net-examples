using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege object:
        // Start with all permissions forbidden, then enable printing only.
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowPrint = true;   // allow printing
        privilege.AllowCopy = false;   // explicitly disallow copying (default for ForbidAll, set for clarity)

        // Apply the privilege using PdfFileSecurity (facade API)
        // The constructor takes the source and destination file names.
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            bool result = fileSecurity.SetPrivilege(privilege);
            if (!result)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
                return;
            }
        }

        Console.WriteLine($"Document privileges updated and saved to '{outputPath}'.");
    }
}