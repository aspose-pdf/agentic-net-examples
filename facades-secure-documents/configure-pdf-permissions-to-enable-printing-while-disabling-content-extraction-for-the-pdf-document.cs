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

        // Initialize the facade with source and destination PDF files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Set privilege: enable printing, disable content extraction (copy)
        // DocumentPrivilege.Print allows printing only.
        bool result = fileSecurity.SetPrivilege(DocumentPrivilege.Print);

        if (result)
        {
            Console.WriteLine($"Permissions applied successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to apply permissions.");
        }
    }
}