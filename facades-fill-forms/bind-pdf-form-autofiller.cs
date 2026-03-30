using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Create an AutoFiller instance (the correct class for automatic form operations)
        AutoFiller filler = new AutoFiller();

        // Bind the PDF form from the specified file path
        filler.BindPdf(inputPdfPath);

        Console.WriteLine("PDF form successfully bound to AutoFiller.");
    }
}
