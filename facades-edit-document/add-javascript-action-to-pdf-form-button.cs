using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string buttonName = "CalcButton";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that reads Quantity and UnitPrice fields, calculates Total, and sets the result.
        string jsCode = "var qty = this.getField('Quantity').value;" +
                        "var price = this.getField('UnitPrice').value;" +
                        "var total = qty * price;" +
                        "this.getField('Total').value = total;";

        // Open the PDF, add the JavaScript to the specified button, and save.
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

        Console.WriteLine($"PDF saved with JavaScript action to '{outputPdf}'.");
    }
}