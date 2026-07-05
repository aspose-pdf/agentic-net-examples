using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "sum_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required before adding fields)
            Page page = doc.Pages.Add();

            // Create first number field
            NumberField field1 = new NumberField(page,
                new Aspose.Pdf.Rectangle(100, 700, 200, 720));
            field1.PartialName = "field1";
            field1.Contents = "Field 1";
            doc.Form.Add(field1);

            // Create second number field
            NumberField field2 = new NumberField(page,
                new Aspose.Pdf.Rectangle(100, 650, 200, 670));
            field2.PartialName = "field2";
            field2.Contents = "Field 2";
            doc.Form.Add(field2);

            // Create result field (read‑only)
            TextBoxField sumField = new TextBoxField(page,
                new Aspose.Pdf.Rectangle(100, 600, 200, 620));
            sumField.PartialName = "sum";
            sumField.Contents = "Sum";
            sumField.ReadOnly = true; // prevent manual editing
            doc.Form.Add(sumField);

            // Attach JavaScript to calculate the sum
            // The script runs whenever the form is recalculated
            string js = @"
                var f1 = this.getField('field1').value;
                var f2 = this.getField('field2').value;
                // Convert to numbers (handles empty strings)
                var n1 = isNaN(parseFloat(f1)) ? 0 : parseFloat(f1);
                var n2 = isNaN(parseFloat(f2)) ? 0 : parseFloat(f2);
                event.value = n1 + n2;
            ";
            sumField.Actions.OnCalculate = new JavascriptAction(js);

            // Ensure automatic recalculation when any field changes
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sum calculation saved to '{outputPath}'.");
    }
}