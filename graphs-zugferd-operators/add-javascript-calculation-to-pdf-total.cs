using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Needed for JavascriptAction

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form (AcroForm) of the document
            var form = doc.Form;

            // Names of the line‑item fields that contain numeric values
            string[] lineItemNames = new[] { "price1", "price2", "price3" };

            // Name of the summary field that will display the total
            const string summaryFieldName = "total";

            // Retrieve the summary field if it exists; otherwise create it
            TextBoxField totalField;
            if (form[summaryFieldName] is TextBoxField existingField)
                totalField = existingField;
            else
            {
                // Create a new textbox field on the first page
                totalField = new TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(100, 700, 200, 720))
                {
                    PartialName = summaryFieldName
                };
                form.Add(totalField);
            }

            // Build JavaScript that sums the values of the line‑item fields
            StringBuilder js = new StringBuilder();
            js.AppendLine("var sum = 0;");
            js.AppendLine("var fields = [");
            for (int i = 0; i < lineItemNames.Length; i++)
            {
                js.Append($"\"{lineItemNames[i]}\"");
                if (i < lineItemNames.Length - 1) js.Append(", ");
            }
            js.AppendLine("];");
            js.AppendLine("for (var i = 0; i < fields.length; i++) {");
            js.AppendLine("  var f = this.getField(fields[i]);");
            js.AppendLine("  if (f && f.value) {");
            js.AppendLine("    var v = parseFloat(f.value);");
            js.AppendLine("    if (!isNaN(v)) sum += v;");
            js.AppendLine("  }");
            js.AppendLine("}");
            js.AppendLine($"this.getField(\"{summaryFieldName}\").value = sum;");

            // Assign the JavaScript to the summary field's calculate action
            totalField.Actions.OnCalculate = new JavascriptAction(js.ToString());

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
