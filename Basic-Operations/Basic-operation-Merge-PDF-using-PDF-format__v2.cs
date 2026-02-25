using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify input files exist
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

        // Merge PDFs using nested using blocks (document-disposal-with-using rule)
        using (Document target = new Document(firstPdf))
        using (Document source = new Document(secondPdf))
        {
            target.Pages.Add(source.Pages);
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}