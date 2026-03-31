using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the TaxRate field
            TextBoxField taxRateField = doc.Form["TaxRate"] as TextBoxField;
            if (taxRateField == null)
            {
                Console.Error.WriteLine("Field 'TaxRate' not found.");
                return;
            }

            // Make the field read‑only
            taxRateField.ReadOnly = true;

            // Attach JavaScript to calculate TaxRate from Subtotal (e.g., 7% tax)
            JavascriptAction calculateScript = new JavascriptAction("event.value = this.getField('Subtotal').value * 0.07;");
            taxRateField.Actions.OnCalculate = calculateScript;

            // Ensure automatic recalculation is enabled (default is true)
            doc.Form.AutoRecalculate = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Updated PDF saved to '" + outputPath + "'.");
    }
}