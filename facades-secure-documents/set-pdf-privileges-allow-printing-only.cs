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

        // Initialize the facade with source and destination files
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            // Set privilege to allow printing only (copying is disallowed)
            bool result = fileSecurity.SetPrivilege(DocumentPrivilege.Print);
            if (!result)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
                return;
            }

            // Save the modified PDF (explicit call, though SetPrivilege already writes the output)
            fileSecurity.Save(outputPath);
        }

        Console.WriteLine($"Document privileges updated and saved to '{outputPath}'.");
    }
}