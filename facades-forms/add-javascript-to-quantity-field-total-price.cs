using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that recalculates TotalPrice whenever Quantity changes.
        // Assumes a hidden field "UnitPrice" holds the unit price.
        string js = @"
var qty = this.getField('Quantity').value;
var unit = this.getField('UnitPrice').value;
var total = parseFloat(qty) * parseFloat(unit);
this.getField('TotalPrice').value = total;
";

        // Load the PDF, attach the script to the Quantity field, and save.
        using (Document doc = new Document(inputPdf))
        using (FormEditor editor = new FormEditor(doc))
        {
            // Attach JavaScript to the Quantity field.
            editor.AddFieldScript("Quantity", js);

            // Save the updated PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPdf}'.");
    }
}