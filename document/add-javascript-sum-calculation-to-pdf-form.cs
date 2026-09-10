using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "SumForm.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a single page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Define rectangles for the three fields (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect1   = new Aspose.Pdf.Rectangle(100, 700, 250, 730); // Field 1
            Aspose.Pdf.Rectangle rect2   = new Aspose.Pdf.Rectangle(100, 650, 250, 680); // Field 2
            Aspose.Pdf.Rectangle rectSum = new Aspose.Pdf.Rectangle(100, 600, 250, 630); // Sum field (read‑only)

            // Create two numeric input fields – note the constructor requires the *Page* instance, not the Document
            NumberField field1 = new NumberField(page, rect1);
            field1.PartialName = "field1";
            field1.AlternateName = "First Number";
            doc.Form.Add(field1);

            NumberField field2 = new NumberField(page, rect2);
            field2.PartialName = "field2";
            field2.AlternateName = "Second Number";
            doc.Form.Add(field2);

            // Create a read‑only text box to display the result
            TextBoxField sumField = new TextBoxField(page, rectSum);
            sumField.PartialName = "sum";
            sumField.AlternateName = "Sum";
            sumField.ReadOnly = true;               // Prevent user editing
            sumField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(sumField);

            // JavaScript that calculates the sum and assigns it to the 'sum' field
            string js = @"
                var v1 = this.getField('field1').value;
                var v2 = this.getField('field2').value;
                var n1 = parseFloat(v1);
                var n2 = parseFloat(v2);
                if (isNaN(n1)) n1 = 0;
                if (isNaN(n2)) n2 = 0;
                this.getField('sum').value = (n1 + n2).toString();
            ";

            // Attach the JavaScript to the OnCalculate action of both input fields
            field1.Actions.OnCalculate = new JavascriptAction(js);
            field2.Actions.OnCalculate = new JavascriptAction(js);

            // Ensure automatic recalculation when any field changes (default is true)
            doc.Form.AutoRecalculate = true;

            // Save the PDF (using the standard Save method inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sum calculation saved to '{outputPath}'.");
    }
}
