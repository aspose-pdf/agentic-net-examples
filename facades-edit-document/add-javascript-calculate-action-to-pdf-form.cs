using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PDF content editor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdf);

        // JavaScript that assigns a Calculate action to the "Quantity" field.
        // It multiplies the value of the "Price" field by the value of the "Quantity" field
        // and stores the result in the "Total" field (or updates the same field).
        string jsCode = @"
            var qtyField = this.getField('Quantity');
            qtyField.setAction('Calculate', 
                'var price = this.getField(""Price"").value; ' +
                'var quantity = this.getField(""Quantity"").value; ' +
                'event.value = price * quantity;');
        ";

        // Add the JavaScript to be executed when the document is opened.
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"PDF saved with JavaScript action: {outputPdf}");
    }
}