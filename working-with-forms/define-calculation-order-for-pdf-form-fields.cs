using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavaScript actions live here

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

        // Load the PDF containing form fields
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form
            if (doc.Form == null)
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            // ------------------------------------------------------------
            // 1. Ensure the fields exist (price, quantity, total)
            // ------------------------------------------------------------
            // Helper to create a TextBoxField if it does not exist
            TextBoxField EnsureTextBox(Document d, string name, double llx, double lly, double urx, double ury)
            {
                var field = d.Form[name] as TextBoxField;
                if (field == null)
                {
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                    field = new TextBoxField(d.Pages[1], rect) { PartialName = name };
                    d.Form.Add(field);
                }
                return field;
            }

            // Create or retrieve the three fields
            var priceField = EnsureTextBox(doc, "price", 100, 560, 200, 580);
            var quantityField = EnsureTextBox(doc, "quantity", 100, 540, 200, 560);
            var totalField = EnsureTextBox(doc, "total", 100, 520, 200, 540);

            // ------------------------------------------------------------
            // 2. Define the calculation order for dependent fields.
            // ------------------------------------------------------------
            // The CalculatedFields property expects an IEnumerable<Field>.
            // Provide the fields in the order they should be evaluated.
            doc.Form.CalculatedFields = new Field[] { priceField, quantityField, totalField };

            // Enable automatic recalculation when field values change.
            doc.Form.AutoRecalculate = true;

            // ------------------------------------------------------------
            // 3. Add a JavaScript calculation script to the dependent field.
            // ------------------------------------------------------------
            string js = "var p = this.getField('price').value;" +
                        "var q = this.getField('quantity').value;" +
                        "event.value = (p && q) ? (p * q).toString() : '';";
            // The correct class name is JavascriptAction (lower‑case 's')
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // ------------------------------------------------------------
            // 4. Save the updated PDF.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form PDF saved with calculation order to '{outputPath}'.");
    }
}
