using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "encryption_status.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF file to PdfFileInfo to inspect its properties
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(pdfPath);

        // Determine encryption status
        string status = fileInfo.IsEncrypted ? "Encrypted" : "Not encrypted";

        // Write the result to a text file
        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            writer.WriteLine(status);
        }

        Console.WriteLine($"Encryption status written to '{outputPath}'.");
    }
}