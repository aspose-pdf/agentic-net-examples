using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF containing line‑item fields and a summary field
        const string outputPath = "output.pdf";  // PDF with JavaScript attached

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure automatic recalculation when any field changes
            doc.Form.AutoRecalculate = true;

            // Retrieve the summary field (the field that will display the total)
            // Adjust the field name to match the actual name in your PDF
            Field totalField = doc.Form["Total"] as Field;
            if (totalField == null)
            {
                Console.Error.WriteLine("Summary field 'Total' not found or is not a form field.");
                return;
            }

            // JavaScript that sums the values of line‑item fields and assigns the result to the summary field.
            // Replace 'Item1', 'Item2', 'Item3' with the actual partial names of your line‑item fields.
            string jsCode = @"
                var sum = 0;
                var fields = ['Item1','Item2','Item3']; // add all line‑item field names here
                for (var i = 0; i < fields.length; i++) {
                    var f = this.getField(fields[i]);
                    if (f && f.value) {
                        // Convert the field value to a number; ignore non‑numeric entries
                        var val = parseFloat(f.value);
                        if (!isNaN(val)) sum += val;
                    }
                }
                event.value = sum.toFixed(2); // set the total with two decimal places
            ";

            // Assign the JavaScript to the OnCalculate action of the summary field
            totalField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Optionally, force recalculation now so the total appears immediately
            totalField.Recalculate();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
