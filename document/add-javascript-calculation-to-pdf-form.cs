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
        // Paths for the output PDF
        const string outputPath = "FormWithTotal.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rectField1 = new Aspose.Pdf.Rectangle(100, 700, 250, 720);
            Aspose.Pdf.Rectangle rectField2 = new Aspose.Pdf.Rectangle(100, 650, 250, 670);
            Aspose.Pdf.Rectangle rectTotal   = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // Create first numeric field
            NumberField field1 = new NumberField(page, rectField1);
            field1.PartialName = "Field1";
            field1.AlternateName = "First Number";
            doc.Form.Add(field1);

            // Create second numeric field
            NumberField field2 = new NumberField(page, rectField2);
            field2.PartialName = "Field2";
            field2.AlternateName = "Second Number";
            doc.Form.Add(field2);

            // Create a read‑only field to display the total
            TextBoxField totalField = new TextBoxField(page, rectTotal);
            totalField.PartialName = "Total";
            totalField.AlternateName = "Sum of Numbers";
            totalField.ReadOnly = true;               // Prevent user editing
            totalField.Color = Aspose.Pdf.Color.LightGray; // Visual cue
            doc.Form.Add(totalField);

            // JavaScript that sums the two numeric fields and writes the result to the total field
            string jsCode = @"
                var f1 = this.getField('Field1').value;
                var f2 = this.getField('Field2').value;
                var n1 = parseFloat(f1);
                var n2 = parseFloat(f2);
                if (isNaN(n1)) n1 = 0;
                if (isNaN(n2)) n2 = 0;
                event.value = (n1 + n2).toString();
            ";

            // Attach the JavaScript to the OnCalculate event of the total field.
            // This event is triggered whenever the form is recalculated (e.g., when other fields change).
            totalField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Ensure automatic recalculation is enabled (default is true, set explicitly for clarity)
            doc.Form.AutoRecalculate = true;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total saved to '{outputPath}'.");
    }
}
