using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form object
            Form form = doc.Form;

            // Create a hidden calculation field (NumberField) on page 1
            // Aspose.Pdf.Rectangle with zero size places the field off‑page (hidden)
            NumberField totalField = new NumberField(doc, new Aspose.Pdf.Rectangle(0, 0, 0, 0));

            // Set the field name (used in JavaScript)
            totalField.Name = "Total";

            // Make the field read‑only (user cannot edit it)
            totalField.ReadOnly = true;

            // Hide the field by setting the Hidden flag (use the enum, not an int)
            totalField.Flags = AnnotationFlags.Hidden;

            // Define the JavaScript that sums Item1 and Item2 values
            // The script uses the Acrobat JavaScript API:
            //   this.getField('fieldName').value  – gets the field value
            //   event.value = ...                 – sets the calculated value
            string js = @"
                var v1 = this.getField('Item1').value;
                var v2 = this.getField('Item2').value;
                // Convert to numbers (handles empty strings)
                v1 = isNaN(v1) ? 0 : Number(v1);
                v2 = isNaN(v2) ? 0 : Number(v2);
                event.value = v1 + v2;
            ";

            // Attach the JavaScript to the OnCalculate action of the field
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Add the field to the form (page number is 1 because we used the document constructor)
            form.Add(totalField, 1);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden calculation field added and saved to '{outputPath}'.");
    }
}
