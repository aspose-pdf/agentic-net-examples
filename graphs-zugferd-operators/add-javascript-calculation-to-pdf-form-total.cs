using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice.pdf";
        const string outputPath = "invoice_with_calc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the summary field where the total will be displayed
            // Assumes a field named "Total" already exists in the form
            var totalField = doc.Form["Total"];
            if (totalField == null)
            {
                Console.Error.WriteLine("Summary field 'Total' not found in the PDF form.");
                return;
            }

            // JavaScript that sums up line‑item fields and writes the result to the summary field.
            // Adjust the field names (ItemPrice1, ItemPrice2, ...) to match the actual PDF.
            string jsCode = @"
                var sum = 0;
                var f1 = this.getField('ItemPrice1');
                if (f1 && f1.value) sum += parseFloat(f1.value);
                var f2 = this.getField('ItemPrice2');
                if (f2 && f2.value) sum += parseFloat(f2.value);
                var f3 = this.getField('ItemPrice3');
                if (f3 && f3.value) sum += parseFloat(f3.value);
                // Write the calculated total back to the summary field
                var totalFld = this.getField('Total');
                if (totalFld) totalFld.value = sum.toFixed(2);
                // Return the value for the OnCalculate action
                sum;
            ";

            // Assign the JavaScript to the OnCalculate action of the summary field
            JavascriptAction calcAction = new JavascriptAction(jsCode);
            totalField.Actions.OnCalculate = calcAction;

            // Ensure the calculation runs when the document is opened.
            // The built‑in PDF JavaScript method "this.calculate();" forces a form recalculation.
            if (doc.Pages.Count > 0)
            {
                var firstPage = doc.Pages[1]; // Pages collection is 1‑based
                firstPage.Actions.OnOpen = new JavascriptAction("this.calculate();");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript calculation: {outputPath}");
    }
}
