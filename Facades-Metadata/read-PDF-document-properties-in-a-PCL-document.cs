using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pcl";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade for the PCL file.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Read and display common PDF document properties.
            Console.WriteLine($"Title: {pdfInfo.Title}");
            Console.WriteLine($"Author: {pdfInfo.Author}");
            Console.WriteLine($"Creator: {pdfInfo.Creator}");
            Console.WriteLine($"Producer: {pdfInfo.Producer}");
            Console.WriteLine($"Subject: {pdfInfo.Subject}");
            Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            Console.WriteLine($"Number of pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Is encrypted: {pdfInfo.IsEncrypted}");
            Console.WriteLine($"PDF version: {pdfInfo.GetPdfVersion()}");
            Console.WriteLine($"Creation date: {pdfInfo.CreationDate}");
            Console.WriteLine($"Modification date: {pdfInfo.ModDate}");
        }
    }
}