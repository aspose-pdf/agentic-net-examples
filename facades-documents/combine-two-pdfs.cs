using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }

        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        try
        {
            PdfFileEditor pdfEditor = new PdfFileEditor();
            bool result = pdfEditor.Concatenate(firstPdf, secondPdf, outputPdf);
            if (result)
            {
                Console.WriteLine($"PDF files concatenated successfully to '{outputPdf}'.");
            }
            else
            {
                Console.Error.WriteLine("Concatenation failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
