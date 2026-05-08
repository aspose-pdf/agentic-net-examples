using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Create a hidden calculation field (zero‑size rectangle)
            // The field will be named "SumField"
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField sumField = new TextBoxField(doc, hiddenRect)
            {
                PartialName = "SumField",
                ReadOnly    = true   // make it non‑editable
            };

            // Add the field to the form
            form.Add(sumField);

            // JavaScript that sums the values of "Item1" and "Item2" fields
            // and stores the result in the hidden "SumField"
            string jsCode = @"
                var f1 = this.getField('Item1').value;
                var f2 = this.getField('Item2').value;
                var v1 = parseFloat(f1);
                var v2 = parseFloat(f2);
                if (isNaN(v1)) v1 = 0;
                if (isNaN(v2)) v2 = 0;
                this.getField('SumField').value = (v1 + v2).toString();
            ";

            // Assign the JavaScript to the OnCalculate action of the hidden field
            JavascriptAction calcAction = new JavascriptAction(jsCode);
            sumField.Actions.OnCalculate = calcAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden calculation field saved to '{outputPath}'.");
    }
}