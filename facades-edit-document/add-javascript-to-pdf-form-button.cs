using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // PDF containing the form and button
        const string outputPdf = "output_form.pdf";  // PDF with JavaScript attached
        const string buttonName = "calcButton";      // Fully qualified name of the push button

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF for editing using FormEditor (parameterless constructor + BindPdf)
        using (Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // JavaScript that reads the values of fields "price" and "qty",
            // calculates the total, and writes it to the field "total".
            string jsCode = @"
                var price = this.getField('price').value;
                var qty   = this.getField('qty').value;
                var total = price * qty;
                this.getField('total').value = total;
            ";

            // Set (or replace) the script for the specified push button.
            // SetFieldScript replaces any existing script; AddFieldScript would append.
            formEditor.SetFieldScript(buttonName, jsCode);

            // Save the modified PDF to a new file.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript added to button '{buttonName}'. Saved as '{outputPdf}'.");
    }
}