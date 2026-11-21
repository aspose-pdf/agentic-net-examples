using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

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
            // No need to assign a new Form – doc.Form is created lazily when a field is added.

            // Create first numeric text box field
            TextBoxField field1 = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 700, 200, 720))
            {
                PartialName = "field1",
                Value = "0"
            };
            doc.Form.Add(field1);

            // Create second numeric text box field
            TextBoxField field2 = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 660, 200, 680))
            {
                PartialName = "field2",
                Value = "0"
            };
            doc.Form.Add(field2);

            // Create a calculated field that sums field1 and field2
            TextBoxField totalField = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 620, 200, 640))
            {
                PartialName = "total",
                ReadOnly = true // user cannot edit the calculated field
            };

            // JavaScript that computes the running total
            string js = @"
                var f1 = this.getField('field1').value;
                var f2 = this.getField('field2').value;
                var n1 = isNaN(parseFloat(f1)) ? 0 : parseFloat(f1);
                var n2 = isNaN(parseFloat(f2)) ? 0 : parseFloat(f2);
                event.value = (n1 + n2).toString();
            ";

            totalField.Actions.OnCalculate = new JavascriptAction(js);
            doc.Form.Add(totalField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated field saved to '{outputPath}'.");
    }
}
