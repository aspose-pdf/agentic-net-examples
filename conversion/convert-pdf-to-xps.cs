using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions, including XpsSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXpsPath = "output.xps";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and convert it to XPS using default XpsSaveOptions
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            XpsSaveOptions xpsOptions = new XpsSaveOptions(); // default settings
            pdfDoc.Save(outputXpsPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXpsPath}");
    }
}