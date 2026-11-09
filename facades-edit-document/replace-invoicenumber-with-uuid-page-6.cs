using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Generate a UUID to replace the placeholder
        string uuid = Guid.NewGuid().ToString();

        // Use the Facades API to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Replace the exact case‑sensitive string "InvoiceNumber" on page 6
            editor.ReplaceText("InvoiceNumber", uuid, 6);

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Replaced 'InvoiceNumber' with UUID on page 6 and saved to '{outputPath}'.");
    }
}