using System;
using System.IO;
using Aspose.Pdf;
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

        // Generate UUID
        string uuid = Guid.NewGuid().ToString();

        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace "InvoiceNumber" with UUID on page 6
            editor.ReplaceText("InvoiceNumber", 6, uuid);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced text on page 6 and saved to '{outputPath}'.");
    }
}