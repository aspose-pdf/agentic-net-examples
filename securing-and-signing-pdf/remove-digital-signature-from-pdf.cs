using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";
        const string outputPath = "unsigned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document. The using block ensures deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Enable signature sanitization. When true, Aspose.Pdf removes
            // all signature fields (including the digital signature) from the document.
            doc.EnableSignatureSanitization = true;

            // Save the modified PDF. No SaveOptions are needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature removed. Output saved to '{outputPath}'.");
    }
}