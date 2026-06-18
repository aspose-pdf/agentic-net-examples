using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // PDF containing numeric fields
        const string outputPath = "output_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure automatic recalculation is enabled (default is true)
            doc.Form.AutoRecalculate = true;

            // -----------------------------------------------------------------
            // 1. Create a field that will display the total of all numeric fields
            // -----------------------------------------------------------------
            // Position the total field on the first page (adjust coordinates as needed)
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            NumberField totalField = new NumberField(doc, totalRect)
            {
                Name = "TotalField",
                PartialName = "TotalField",
                ReadOnly = true,
                // Background color of the field
                Color = Color.LightGray
            };
            // Add the total field to the form (page 1)
            doc.Form.Add(totalField, 1);

            // Set a simple border – Border requires the parent annotation and has no Color property
            totalField.Border = new Border(totalField) { Width = 1 };

            // -----------------------------------------------------------------
            // 2. Build JavaScript that sums all numeric fields and writes the result
            //    into the total field. Adjust the field names array to match your PDF.
            // -----------------------------------------------------------------
            string[] numericFieldNames = { "Field1", "Field2", "Field3" }; // <-- replace with actual field names
            string script = "var sum = 0;\n";
            foreach (string name in numericFieldNames)
            {
                script += $"var f = this.getField('{name}');\n";
                script += "if (f && !isNaN(parseFloat(f.value))) { sum += parseFloat(f.value); }\n";
            }
            script += "this.getField('TotalField').value = sum.toString();";

            // -----------------------------------------------------------------
            // 3. Attach the JavaScript to the Calculate action of the total field.
            //    This action runs whenever the form is recalculated (e.g., when a
            //    numeric field value changes or when the document is opened).
            // -----------------------------------------------------------------
            JavascriptAction jsAction = new JavascriptAction(script);
            totalField.Actions.OnCalculate = jsAction;

            // -----------------------------------------------------------------
            // 4. (Optional) Force an initial calculation so the total field is populated
            //    when the document is opened. Setting AutoRecalculate = true together
            //    with the OnCalculate action is sufficient; explicit execution is not
            //    required.
            // -----------------------------------------------------------------

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with total calculation saved to '{outputPath}'.");
    }
}
