using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege object that allows everything,
        // then disable copying while keeping printing enabled.
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowCopy = false; // disable copy permission

        // Initialize the PdfFileSecurity facade, load the source PDF,
        // apply the privilege settings, and save the result.
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);               // load source PDF
        bool result = fileSecurity.SetPrivilege(privilege); // apply privileges

        if (!result)
        {
            Console.Error.WriteLine("Failed to set document privileges.");
            return;
        }

        fileSecurity.Save(outputPath); // save the secured PDF
        Console.WriteLine($"PDF saved with copying disabled and printing enabled: {outputPath}");
    }
}