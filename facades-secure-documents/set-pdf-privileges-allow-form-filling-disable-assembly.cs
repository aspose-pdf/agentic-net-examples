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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege that allows form filling but disables document assembly
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowAssembly = false;   // disable assembly
        privilege.AllowFillIn = true;      // ensure form filling is allowed

        // Initialize the security facade and bind the source PDF
        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);

        // Apply the privilege settings
        bool result = security.SetPrivilege(privilege);
        if (!result)
        {
            Console.Error.WriteLine("Failed to set document privileges.");
            return;
        }

        // Save the PDF with the new privileges
        security.Save(outputPath);
        Console.WriteLine($"PDF saved with updated privileges to '{outputPath}'.");
    }
}