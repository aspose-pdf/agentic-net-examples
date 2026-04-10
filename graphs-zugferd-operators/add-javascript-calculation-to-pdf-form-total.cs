using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the form fields
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Ensure the form will recalculate when any field changes
            form.AutoRecalculate = true;

            // Retrieve the summary field where the total will be displayed
            // (Assumes a field named "Total" already exists in the PDF)
            Field totalField = doc.Form["Total"] as Field;
            if (totalField == null)
            {
                Console.Error.WriteLine("Summary field 'Total' not found.");
                return;
            }

            // JavaScript that sums the values of line‑item fields and writes the result
            // Adjust the field names ("Item1", "Item2", "Item3") to match your PDF.
            string jsCode = @"
                var sum = 0;
                var f1 = this.getField('Item1');
                var f2 = this.getField('Item2');
                var f3 = this.getField('Item3');
                if (f1 && f1.value) sum += parseFloat(f1.value);
                if (f2 && f2.value) sum += parseFloat(f2.value);
                if (f3 && f3.value) sum += parseFloat(f3.value);
                this.getField('Total').value = sum.toFixed(2);
            ";

            // Assign the JavaScript to the OnCalculate action of the summary field
            totalField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript calculation saved to '{outputPath}'.");
    }
}
