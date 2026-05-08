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
        const string inputPath = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // First numeric field
            NumberField field1 = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 700, 200, 720));
            field1.PartialName = "Field1";
            field1.Value = "10";

            // Second numeric field
            NumberField field2 = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 650, 200, 670));
            field2.PartialName = "Field2";
            field2.Value = "20";

            // Calculated total field
            NumberField totalField = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 600, 200, 620));
            totalField.PartialName = "Total";

            // JavaScript that sums Field1 and Field2
            JavascriptAction js = new JavascriptAction(
                "event.value = this.getField('Field1').value + this.getField('Field2').value;");
            totalField.Actions.OnCalculate = js;

            // Add fields to the form
            doc.Form.Add(field1);
            doc.Form.Add(field2);
            doc.Form.Add(totalField);

            // Specify calculation order (total field after the others)
            doc.Form.CalculatedFields = new[] { totalField };

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}