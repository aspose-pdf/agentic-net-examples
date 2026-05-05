using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";
        const string outputPdf = "output_form_with_calc.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that assigns a Calculate action to the "Quantity" field.
        // The script multiplies the values of "Quantity" and "Price" fields
        // and stores the result in the "Total" field.
        string jsCode = @"
var qty   = this.getField('Quantity');
var price = this.getField('Price');
var total = this.getField('Total');
qty.setAction('Calculate', 'event.value = this.getField(""Quantity"").value * this.getField(""Price"").value;');
";

        try
        {
            // Bind the existing PDF, add the JavaScript action, and save.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdf);
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);
            editor.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}