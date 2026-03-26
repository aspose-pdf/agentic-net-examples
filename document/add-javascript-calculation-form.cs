using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // First input text box field
            TextBoxField field1 = new TextBoxField(page, new Rectangle(100, 700, 200, 720));
            field1.PartialName = "field1"; // set the field name
            doc.Form.Add(field1);

            // Second input text box field
            TextBoxField field2 = new TextBoxField(page, new Rectangle(100, 650, 200, 670));
            field2.PartialName = "field2"; // set the field name
            doc.Form.Add(field2);

            // Calculated field that will display the sum
            TextBoxField sumField = new TextBoxField(page, new Rectangle(100, 600, 200, 620));
            sumField.PartialName = "sumField"; // set the field name
            doc.Form.Add(sumField);

            // JavaScript action to compute the sum of field1 and field2
            string javaScript = "event.value = (parseFloat(this.getField('field1').value) + parseFloat(this.getField('field2').value)).toString();";
            sumField.Actions.OnCalculate = new JavascriptAction(javaScript);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}