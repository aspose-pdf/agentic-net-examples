using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_sum.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to host the form fields
            Aspose.Pdf.Page page = doc.Pages.Add();

            // First input field
            Aspose.Pdf.Forms.TextBoxField field1 = new Aspose.Pdf.Forms.TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(100, 700, 250, 720));
            field1.PartialName = "Field1";
            field1.Value = "0";
            doc.Form.Add(field1, 1);

            // Second input field
            Aspose.Pdf.Forms.TextBoxField field2 = new Aspose.Pdf.Forms.TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(100, 650, 250, 670));
            field2.PartialName = "Field2";
            field2.Value = "0";
            doc.Form.Add(field2, 1);

            // Result field (read‑only)
            Aspose.Pdf.Forms.TextBoxField resultField = new Aspose.Pdf.Forms.TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(100, 600, 250, 620));
            resultField.PartialName = "Result";
            resultField.Value = "";
            resultField.ReadOnly = true;
            doc.Form.Add(resultField, 1);

            // JavaScript that calculates the sum of Field1 and Field2
            string js = @"
var f1 = this.getField('Field1').value;
var f2 = this.getField('Field2').value;
var sum = parseFloat(f1) + parseFloat(f2);
this.getField('Result').value = sum;
";

            // Attach the script to both input fields (executed when the field loses focus)
            JavascriptAction calcAction = new JavascriptAction(js);
            field1.Actions.OnExit = calcAction; // runs when user leaves Field1
            field2.Actions.OnExit = calcAction; // runs when user leaves Field2

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
