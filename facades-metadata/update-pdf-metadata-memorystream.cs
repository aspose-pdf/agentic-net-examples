using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Read the source PDF into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        {
            // Bind the PDF from the memory stream to PdfFileInfo
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(ms);

                // Modify metadata
                pdfInfo.Title  = "New Document Title";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Updated metadata via PdfFileInfo";
                pdfInfo.Keywords = "Aspose.Pdf, Metadata, MemoryStream";

                // Save the modified PDF to a new file
                pdfInfo.Save(outputPath);
            }
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}