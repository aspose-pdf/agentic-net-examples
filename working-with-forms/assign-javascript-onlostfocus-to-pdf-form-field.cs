using System;
using System.IO;
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

        using (Document doc = new Document(inputPath))
        {
            // Retrieve an existing text box field or create a new one
            TextBoxField calcField = doc.Form["CalcField"] as TextBoxField;
            if (calcField == null)
            {
                calcField = new TextBoxField(
                    doc.Pages[1],
                    new Aspose.Pdf.Rectangle(100, 700, 250, 720))
                {
                    PartialName = "CalcField",
                    Value = "0"
                };
                doc.Form.Add(calcField);
            }

            // JavaScript to calculate the sum of two other fields (Field1 and Field2)
            // and assign the result to this field when it loses focus (OnLostFocus event)
            string js = @"
                var f1 = this.getField('Field1').value;
                var f2 = this.getField('Field2').value;
                var sum = Number(f1) + Number(f2);
                this.value = sum;
            ";

            // Assign the JavaScript action to the OnLostFocus event of the field
            calcField.Actions.OnLostFocus = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action: {outputPath}");
    }
}
