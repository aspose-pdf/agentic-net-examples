using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction is defined here

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

        // Load existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable automatic recalculation of calculated fields (default is true)
            doc.Form.AutoRecalculate = true;

            // Create a hidden calculation field (zero‑size rectangle)
            NumberField sumField = new NumberField(doc, new Aspose.Pdf.Rectangle(0, 0, 0, 0))
            {
                Name = "SumField",
                ReadOnly = true   // prevent user editing
            };

            // JavaScript that sums Item1 and Item2 and stores result in SumField
            string js = @"
                var v1 = this.getField('Item1').value;
                var v2 = this.getField('Item2').value;
                var n1 = parseFloat(v1);
                var n2 = parseFloat(v2);
                if (isNaN(n1)) n1 = 0;
                if (isNaN(n2)) n2 = 0;
                this.getField('SumField').value = n1 + n2;
            ";

            // Assign the JavaScript to the OnCalculate action of the field
            sumField.Actions.OnCalculate = new JavascriptAction(js);

            // Add the field to the form
            doc.Form.Add(sumField);

            // No explicit Recalculate call – automatic recalculation is handled by AutoRecalculate

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden calculation field added and saved to '{outputPath}'.");
    }
}
