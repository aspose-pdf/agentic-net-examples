using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for JavascriptAction

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create first numeric text field
            TextBoxField field1 = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 700, 250, 720))
            {
                PartialName = "field1",
                Value = "0"
            };
            doc.Form.Add(field1);

            // Create second numeric text field
            TextBoxField field2 = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 660, 250, 680))
            {
                PartialName = "field2",
                Value = "0"
            };
            doc.Form.Add(field2);

            // Create a result field to display the sum
            TextBoxField result = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 620, 250, 640))
            {
                PartialName = "result",
                Value = ""
            };
            doc.Form.Add(result);

            // Create a button that will execute JavaScript on click
            ButtonField button = new ButtonField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 580, 250, 600))
            {
                PartialName = "calcButton",
                Value = "Calculate"
            };

            // JavaScript: read the two fields, compute sum, write to result field
            string js = @"
var f1 = this.getField('field1').value;
var f2 = this.getField('field2').value;
var sum = parseFloat(f1) + parseFloat(f2);
this.getField('result').value = sum;
";
            // Assign the script to the button's press action (supported property)
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            doc.Form.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with button annotation saved to '{outputPath}'.");
    }
}
