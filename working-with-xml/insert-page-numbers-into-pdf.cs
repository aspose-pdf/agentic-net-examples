using System;
using System.IO;
using Aspose.Pdf;
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

        // ------------------------------------------------------------
        // 1. Create the PdfFileStamp facade (no using – it does not implement IDisposable)
        // ------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();

        // 2. Bind the source PDF file
        fileStamp.BindPdf(inputPath);

        // 3. (Optional) Set the starting page number – default is 1
        fileStamp.StartingNumber = 1;

        // 4. Add page numbers.
        //    The format string may contain the '#' placeholder which will be replaced
        //    by the actual page number during stamping.
        fileStamp.AddPageNumber("Page #");

        // 5. Save the stamped PDF to the desired output file
        fileStamp.Save(outputPath);

        // 6. Close the facade (releases internal resources)
        fileStamp.Close();

        Console.WriteLine($"Page numbers added successfully. Output saved to '{outputPath}'.");
    }
}