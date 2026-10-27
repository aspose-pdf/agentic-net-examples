using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Color
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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Define the rectangle where the calculated total field will appear
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(50, 750, 200, 770);

            // Create a read‑only text box that will hold the running total
            TextBoxField totalField = new TextBoxField(doc.Pages[1], totalRect);
            totalField.PartialName = "TotalSum";
            totalField.Value = "0";
            totalField.ReadOnly = true;

            // Set the default appearance (font, size, color) for the text box
            // DefaultAppearance constructor expects (string fontName, double fontSize, System.Drawing.Color)
            totalField.DefaultAppearance = new DefaultAppearance(
                "Helvetica",
                12,
                System.Drawing.Color.Black);

            // JavaScript that iterates over all form fields, sums numeric values,
            // and assigns the result to this field (event.value)
            string jsCalculate = @"
                var sum = 0;
                for (var i = 0; i < this.numFields; i++) {
                    var f = this.getField(this.getFieldName(i));
                    if (f && f.type == 'text' && f !== this) {
                        var v = parseFloat(f.value);
                        if (!isNaN(v)) {
                            sum += v;
                        }
                    }
                }
                event.value = sum.toString();
            ";

            // Attach the JavaScript to the OnCalculate action of the total field
            totalField.Actions.OnCalculate = new JavascriptAction(jsCalculate);

            // Add the field to the page's annotations collection
            doc.Form.Add(totalField, 1);

            // Save the modified PDF; Document.Save writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"Calculated total field added. Output saved to '{outputPath}'.");
    }
}
