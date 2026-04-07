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
        const string searchText = "InvoiceNumber";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string replacement = Guid.NewGuid().ToString();

        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);
            bool replaced = editor.ReplaceText(searchText, 6, replacement);
            if (!replaced)
            {
                Console.WriteLine("Text not found on page 6.");
            }
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced '{searchText}' with UUID on page 6. Saved to '{outputPath}'.");
    }
}