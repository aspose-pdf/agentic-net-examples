using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keywords   = "Sample, Aspose, PDF";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF file information, set the Keywords metadata, and save to a new file
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(inputPath);      // Load existing PDF
        fileInfo.Keywords = keywords;     // Set the Keywords field
        fileInfo.Save(outputPath);        // Save the updated PDF

        // Verify that the Keywords were written correctly by reading them back
        PdfFileInfo verifyInfo = new PdfFileInfo();
        verifyInfo.BindPdf(outputPath);
        Console.WriteLine($"Keywords set: {verifyInfo.Keywords}");
    }
}