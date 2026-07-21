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
        const string outputPath = "output_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Ensure automatic recalculation is enabled (default is true)
            form.AutoRecalculate = true;

            // Names of the numeric fields that should be summed
            string[] numericFieldNames = { "Field1", "Field2", "Field3" };
            // Name of the field that will display the total
            const string totalFieldName = "Total";

            // Ensure the total field exists; create it if missing
            if (!form.HasField(totalFieldName))
            {
                // Place the total field on the first page at an arbitrary location
                // Fully qualify Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);
                TextBoxField totalField = new TextBoxField(doc.Pages[1], totalRect)
                {
                    PartialName = totalFieldName,
                    // Optional: make the field read‑only so users cannot edit the total manually
                    ReadOnly = true
                };
                form.Add(totalField);
            }

            // Build the JavaScript that sums the numeric fields and writes the result to the total field
            // The script will be attached to each numeric field's Calculate action
            string js = "var sum = 0;";
            foreach (string name in numericFieldNames)
            {
                js += $" var val = this.getField(\"{name}\").value; ";
                js += " if (!isNaN(val)) { sum += Number(val); } ";
            }
            js += $" this.getField(\"{totalFieldName}\").value = sum;";

            // Attach the JavaScript to each numeric field
            foreach (string name in numericFieldNames)
            {
                // Retrieve the field; cast to TextBoxField (numeric fields are typically TextBoxField derivatives)
                if (form.HasField(name))
                {
                    var field = (TextBoxField)form[name];
                    // Assign the JavaScript to the Calculate action
                    field.Actions.OnCalculate = new JavascriptAction(js);
                }
                else
                {
                    Console.WriteLine($"Warning: numeric field \"{name}\" not found in the document.");
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with total calculation saved to '{outputPath}'.");
    }
}