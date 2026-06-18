using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_calc.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using PdfContentEditor (facade)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // JavaScript that assigns a Calculate action to the "Quantity" field.
        // When the field value changes, Total = Price * Quantity is computed.
        string js = @"
var qty   = this.getField('Quantity');
var price = this.getField('Price');
var total = this.getField('Total');
if (qty && price && total) {
    qty.setAction('Calculate', 'event.value = price.value * qty.value; total.value = event.value;');
}
";

        // Attach the script to the document open event so it runs when the PDF is opened.
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, js);

        // Save the modified PDF.
        editor.Save(outputPdf);

        Console.WriteLine($"PDF saved with JavaScript calculation: {outputPdf}");
    }
}