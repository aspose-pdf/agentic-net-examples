using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // JavaScript that calculates TotalPrice = Quantity * UnitPrice
        string js = "var qty = this.getField('Quantity').value;" +
                    "var unit = this.getField('UnitPrice').value;" +
                    "this.getField('TotalPrice').value = qty * unit;";

        // Retrieve the Quantity field (assumed to be a TextBoxField)
        if (doc.Form["Quantity"] is TextBoxField quantityField)
        {
            // Attach the script to the field's Calculate action
            quantityField.Actions.OnCalculate = new JavascriptAction(js);
        }
        else
        {
            Console.Error.WriteLine("Quantity field not found or not a TextBoxField.");
        }

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
