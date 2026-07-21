using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind the document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Generate a new UUID and replace the exact text on page 6
                string uuid = Guid.NewGuid().ToString();
                editor.ReplaceText("InvoiceNumber", 6, uuid);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced 'InvoiceNumber' with UUID on page 6. Saved to '{outputPath}'.");
    }
}