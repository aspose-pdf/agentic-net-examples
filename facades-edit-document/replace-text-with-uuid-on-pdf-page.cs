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
            // Initialize the content editor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Generate a new UUID
                string uuid = Guid.NewGuid().ToString();

                // Replace the exact text "InvoiceNumber" on page 6 with the UUID
                editor.ReplaceText("InvoiceNumber", 6, uuid);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replacement complete. Saved to '{outputPath}'.");
    }
}