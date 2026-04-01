using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF into PdfFileSecurity (parameterless constructor, then BindPdf)
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);

        // Create a privilege object – allow printing only, disallow modifying contents
        DocumentPrivilege privilege = DocumentPrivilege.Print;
        privilege.AllowModifyContents = false;

        try
        {
            // Set the privilege; this will throw if it fails (no AllowExceptions used)
            bool success = fileSecurity.SetPrivilege(privilege);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set privilege.");
                return;
            }

            // Save the modified PDF
            fileSecurity.Save(outputPath);
            Console.WriteLine($"Privilege modified and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}