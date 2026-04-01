using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Load the PDF into the security facade
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);

        // Create privilege: allow printing, disallow copying
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowPrint = true;
        privilege.AllowCopy = false;

        // Apply the privilege settings
        bool success = fileSecurity.SetPrivilege(privilege);
        if (!success)
        {
            Console.Error.WriteLine("Failed to set privileges.");
            return;
        }

        // Save the modified PDF
        fileSecurity.Save(outputPath);
        Console.WriteLine($"Privileges set and saved to '{outputPath}'.");
    }
}
