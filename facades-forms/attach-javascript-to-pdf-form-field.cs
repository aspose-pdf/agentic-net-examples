using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Retrieve the required form fields
        var quantityField = doc.Form["Quantity"];
        var totalPriceField = doc.Form["TotalPrice"];
        if (quantityField == null || totalPriceField == null)
        {
            Console.Error.WriteLine("Required fields 'Quantity' or 'TotalPrice' not found in the PDF.");
            return;
        }

        // JavaScript that recalculates TotalPrice whenever Quantity or UnitPrice changes.
        // The script is attached to the TotalPrice field's OnCalculate action.
        string jsCode = @"
            var qty = this.getField('Quantity').value;
            var price = this.getField('UnitPrice').value;
            var q = parseFloat(qty);
            var p = parseFloat(price);
            if (!isNaN(q) && !isNaN(p)) {
                event.value = (q * p).toString();
            } else {
                event.value = '';
            }
        ";

        // Attach the script to the TotalPrice field's OnCalculate action (valid property).
        totalPriceField.Actions.OnCalculate = new JavascriptAction(jsCode);

        // Save the modified PDF
        doc.Save(outputPdf);

        Console.WriteLine($"PDF with JavaScript saved to '{outputPdf}'.");
    }
}
