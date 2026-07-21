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

        // PdfFileSecurity works as a facade for setting security on a PDF file.
        // The constructor receives the source PDF and the destination path.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // DocumentPrivilege.Print allows printing only; copying is not permitted.
            bool ok = security.SetPrivilege(DocumentPrivilege.Print);
            if (!ok)
            {
                Console.Error.WriteLine("Failed to apply privileges.");
                return;
            }

            // Save the modified PDF. The Save method writes the output file.
            security.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with printing allowed and copying disabled: {outputPath}");
    }
}