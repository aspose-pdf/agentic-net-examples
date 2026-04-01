using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a privilege object that forbids all actions
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        // Enable printing
        privilege.AllowPrint = true;
        // Ensure copying is disabled
        privilege.AllowCopy = false;

        // Apply the privilege to the PDF
        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);
        security.SetPrivilege(privilege);
        security.Save(outputPath);

        Console.WriteLine("Copy disabled, printing enabled. Saved to " + outputPath);
    }
}
