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

        // Load the PDF to ensure the form is accessible and enable auto‑recalculation
        using (Document doc = new Document(inputPath))
        {
            doc.Form.AutoRecalculate = true;
        }

        // Add a document‑level JavaScript that calculates Total = Price * Quantity
        string javaScript = @"
            var qty = this.getField('Quantity');
            var price = this.getField('Price');
            var total = this.getField('Total');
            qty.setAction('Calculate', 'event.value = price.value * qty.value;');
            total.setAction('Calculate', 'event.value = price.value * qty.value;');
        ";

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, javaScript);
        editor.Save(outputPath);

        Console.WriteLine($"JavaScript calculation added and saved to '{outputPath}'.");
    }
}