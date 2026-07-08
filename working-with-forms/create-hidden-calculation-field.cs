using System;
using System.IO;
using Aspose.Pdf;                       // Core PDF classes
using Aspose.Pdf.Forms;                 // Form field classes
using Aspose.Pdf.Annotations;           // JavascriptAction, HideAction
using Aspose.Pdf.Drawing;               // Rectangle for field placement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Existing PDF with Item1 & Item2 fields
        const string outputPath = "output_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a hidden calculation field named "Total"
            // -----------------------------------------------------------------
            // Place the field at (0,0,0,0) so it is not visible on the page.
            // The field is read‑only because its value is calculated by JavaScript.
            NumberField totalField = new NumberField(doc,
                new Aspose.Pdf.Rectangle(0, 0, 0, 0))
            {
                PartialName = "Total",
                ReadOnly    = true
            };

            // -----------------------------------------------------------------
            // 2. Attach JavaScript that sums the values of Item1 and Item2
            // -----------------------------------------------------------------
            // The script runs whenever the field needs to be calculated.
            // It accesses the other fields by their full names (partial names are sufficient here).
            string js = @"
                var v1 = this.getField('Item1').value;
                var v2 = this.getField('Item2').value;
                // Convert to numbers (handles empty strings)
                var n1 = isNaN(parseFloat(v1)) ? 0 : parseFloat(v1);
                var n2 = isNaN(parseFloat(v2)) ? 0 : parseFloat(v2);
                event.value = n1 + n2;
            ";
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // -----------------------------------------------------------------
            // 3. Add the field to the document's form
            // -----------------------------------------------------------------
            doc.Form.Add(totalField);

            // -----------------------------------------------------------------
            // 4. (Optional) Ensure automatic recalculation is enabled
            // -----------------------------------------------------------------
            doc.Form.AutoRecalculate = true;   // default is true, set explicitly for clarity

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden calculation field: {outputPath}");
    }
}