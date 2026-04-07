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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Enable automatic recalculation of form fields
            doc.Form.AutoRecalculate = true;

            // Retrieve the field that will display the total amount
            // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
            Field totalField = doc.Form["Total"] as Field;
            if (totalField != null)
            {
                // JavaScript that multiplies Quantity and Price fields and assigns the result to Total
                string javaScript = "event.value = this.getField('Quantity').value * this.getField('Price').value;";
                totalField.Actions.OnCalculate = new JavascriptAction(javaScript);
            }
            else
            {
                Console.Error.WriteLine("Field 'Total' not found in the form.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with JavaScript to '{outputPath}'.");
    }
}
