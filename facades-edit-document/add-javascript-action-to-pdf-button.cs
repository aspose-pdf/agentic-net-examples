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
        const string buttonName = "CalcButton";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the FormEditor facade and bind the document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // JavaScript that reads Quantity and UnitPrice fields,
                // calculates the total, and writes it to the Total field
                string js = @"
var qty = this.getField('Quantity').value;
var price = this.getField('UnitPrice').value;
if (!isNaN(qty) && !isNaN(price)) {
    this.getField('Total').value = qty * price;
} else {
    app.alert('Please enter valid numbers for Quantity and Unit Price.');
}";

                // Attach the script to the push button field
                formEditor.AddFieldScript(buttonName, js);

                // Save the modified PDF with the JavaScript action
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with JavaScript action: {outputPdf}");
    }
}