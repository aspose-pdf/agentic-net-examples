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

        // Use a using block to guarantee disposal of the PdfFileStamp facade.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF.
            fileStamp.BindPdf(inputPath);

            // Add a header that automatically inserts the PDF file name.
            // The placeholder {file_name} is replaced by Aspose.Pdf with the actual file name.
            fileStamp.AddHeader("{file_name}", 20f);

            // Save the stamped PDF.
            fileStamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
