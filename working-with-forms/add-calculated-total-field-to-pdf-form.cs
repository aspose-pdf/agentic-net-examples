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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Create first numeric field
            NumberField field1 = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 700, 200, 720));
            field1.PartialName = "Field1";
            field1.Value = "0";
            form.Add(field1);

            // Create second numeric field
            NumberField field2 = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 650, 200, 670));
            field2.PartialName = "Field2";
            field2.Value = "0";
            form.Add(field2);

            // Create third numeric field
            NumberField field3 = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 600, 200, 620));
            field3.PartialName = "Field3";
            field3.Value = "0";
            form.Add(field3);

            // Create total field that sums the three numeric fields
            NumberField totalField = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 550, 200, 570));
            totalField.PartialName = "Total";
            totalField.ReadOnly = true; // make the total field read‑only

            // JavaScript to calculate the running total
            string js = "event.value = " +
                        "parseFloat(this.getField('Field1').value || 0) + " +
                        "parseFloat(this.getField('Field2').value || 0) + " +
                        "parseFloat(this.getField('Field3').value || 0);";

            totalField.Actions.OnCalculate = new JavascriptAction(js);
            form.Add(totalField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Calculated field added and saved to '{outputPath}'.");
    }
}