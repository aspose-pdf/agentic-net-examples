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

        // PdfFileStamp is a facade that handles loading and saving internally.
        // It does not implement IDisposable, so we do not wrap it in a using block.
        // Use BindPdf to load the source PDF, AddPageNumber to add the stamp,
        // then Save the result and finally close the facade.
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Load the source PDF.
        fileStamp.BindPdf(inputPath);

        // Add a page number stamp.
        // Aspose.Pdf uses the '#' character as a placeholder for the current page number.
        // The requested {page_number} placeholder is therefore expressed as "#".
        fileStamp.AddPageNumber("Page #");

        // Write the stamped PDF to the output file.
        fileStamp.Save(outputPath);

        // Release resources.
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}