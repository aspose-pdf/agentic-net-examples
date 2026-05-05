using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithSum.pdf";

        // Create a new PDF document and add a page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the two input fields and the result field.
            // Rectangle constructor: (llx, lly, urx, ury)
            Rectangle rectField1 = new Rectangle(50, 700, 200, 730);
            Rectangle rectField2 = new Rectangle(50, 650, 200, 680);
            Rectangle rectResult = new Rectangle(50, 600, 200, 630);

            // Create text box fields using the (Page, Rectangle) constructor.
            TextBoxField field1 = new TextBoxField(page, rectField1);
            field1.PartialName = "Field1";

            TextBoxField field2 = new TextBoxField(page, rectField2);
            field2.PartialName = "Field2";

            TextBoxField resultField = new TextBoxField(page, rectResult);
            resultField.PartialName = "Result";

            // Add the fields to the document's form.
            doc.Form.Add(field1);
            doc.Form.Add(field2);
            doc.Form.Add(resultField);

            // JavaScript that calculates the sum of Field1 and Field2.
            // The script is attached to the result field's OnCalculate action.
            string jsCode = @"
                var f1 = this.getField('Field1').value;
                var f2 = this.getField('Field2').value;
                // Convert to numbers; empty fields are treated as 0.
                var n1 = isNaN(parseFloat(f1)) ? 0 : parseFloat(f1);
                var n2 = isNaN(parseFloat(f2)) ? 0 : parseFloat(f2);
                event.value = n1 + n2;
            ";

            // Assign the JavaScript action to the result field.
            resultField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Optional: make the result field read‑only so the user cannot edit it.
            resultField.ReadOnly = true;

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated sum saved to '{outputPath}'.");
    }
}
