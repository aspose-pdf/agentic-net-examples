using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSecurity and DocumentPrivilege are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileSecurity facade that works on the input file and writes to the output file
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            // Start from a restrictive privilege set
            DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;

            // Enable printing (high‑resolution)
            privilege.AllowPrint = true;
            privilege.PrintAllowLevel = 2;   // 2 = High Resolution

            // Disable copying completely
            privilege.AllowCopy = false;
            privilege.CopyAllowLevel = 0;    // 0 = None

            // Apply the privilege settings to the PDF
            fileSecurity.SetPrivilege(privilege);
        }

        Console.WriteLine($"PDF saved with printing enabled and copying disabled: {outputPath}");
    }
}