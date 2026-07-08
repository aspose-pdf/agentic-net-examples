using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form.pdf";
        const string outputPdf = "form_with_calc.pdf";
        const string buttonName = "CalcButton";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // JavaScript that reads Quantity and UnitPrice fields, multiplies them,
        // and writes the result into the Total field.
        string jsCode = @"
            var qty = this.getField('Quantity').value;
            var price = this.getField('UnitPrice').value;
            var total = qty * price;
            this.getField('Total').value = total;
        ";

        // Use FormEditor (a Facade) to bind the PDF, add the script to the button,
        // and save the modified document.
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);
            bool success = formEditor.AddFieldScript(buttonName, jsCode);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to add JavaScript to button '{buttonName}'.");
            }
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript action: {outputPdf}");
    }
}