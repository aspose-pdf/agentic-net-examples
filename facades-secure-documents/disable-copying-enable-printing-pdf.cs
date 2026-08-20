using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // PdfFileSecurity works with source and destination file paths.
        // It will apply the specified privilege and write the result to outputPath.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // DocumentPrivilege.Print allows printing and disables copying.
            bool result = security.SetPrivilege(DocumentPrivilege.Print);
            if (!result)
            {
                Console.Error.WriteLine("Failed to set document privileges.");
                return;
            }
        }

        Console.WriteLine($"Successfully saved '{outputPath}' with copying disabled and printing enabled.");
    }
}