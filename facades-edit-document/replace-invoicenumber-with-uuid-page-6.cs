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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Generate a new UUID string
        string uuid = Guid.NewGuid().ToString();

        // Facades API: load PDF, replace text on page 6, and save
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);                     // load source PDF
        editor.ReplaceText("InvoiceNumber", uuid, 6);  // case‑sensitive replace on page 6
        editor.Save(outputPath);                       // write modified PDF

        Console.WriteLine($"Replaced 'InvoiceNumber' with UUID on page 6 and saved to '{outputPath}'.");
    }
}