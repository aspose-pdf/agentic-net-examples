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

        // Initialize the facade with source and destination files
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // Allow printing, disallow copying (Copy is not included in DocumentPrivilege.Print)
            security.SetPrivilege(DocumentPrivilege.Print);

            // Persist the changes to the output file
            security.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with printing enabled and copying disabled: {outputPath}");
    }
}