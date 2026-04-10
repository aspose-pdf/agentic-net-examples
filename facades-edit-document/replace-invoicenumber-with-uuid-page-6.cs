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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor facade and bind it to the document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Generate a new UUID string
                string uuid = Guid.NewGuid().ToString();

                // Replace the exact text "InvoiceNumber" on page 6 (1‑based indexing)
                editor.ReplaceText("InvoiceNumber", 6, uuid);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced 'InvoiceNumber' with UUID and saved to '{outputPath}'.");
    }
}