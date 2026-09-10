using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with fields: Price, Quantity, Total
        const string outputPdf = "output.pdf";  // PDF with added button and JavaScript

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use FormEditor (a Facades class) to edit the form.
        // The class implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPdf);

            // Add a push button named "CalcButton" on page 1.
            // Parameters: field type, field name, page number, lower‑left x, lower‑left y, upper‑right x, upper‑right y.
            formEditor.AddField(FieldType.PushButton, "CalcButton", 1, 100, 500, 200, 540);

            // JavaScript that reads "Price" and "Quantity" fields, multiplies them,
            // and writes the result into the "Total" field.
            string jsCode = @"
                var price = this.getField('Price').value;
                var qty   = this.getField('Quantity').value;
                if (!isNaN(price) && !isNaN(qty)) {
                    this.getField('Total').value = price * qty;
                } else {
                    app.alert('Please enter numeric values for Price and Quantity.');
                }";

            // Attach the script to the button. If a script already exists, the new one is appended.
            formEditor.AddFieldScript("CalcButton", jsCode);

            // Save the modified PDF. The Save method writes a PDF regardless of the file extension.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript button saved to '{outputPdf}'.");
    }
}