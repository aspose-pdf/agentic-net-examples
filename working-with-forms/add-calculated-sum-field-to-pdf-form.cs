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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Create first numeric field
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 200, 730);
            NumberField field1 = new NumberField(doc, rect1);
            field1.PartialName = "Field1";
            field1.Value = "10"; // initial value
            form.Add(field1);

            // Create second numeric field
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 650, 200, 680);
            NumberField field2 = new NumberField(doc, rect2);
            field2.PartialName = "Field2";
            field2.Value = "20"; // initial value
            form.Add(field2);

            // Create calculated field that sums Field1 and Field2
            Aspose.Pdf.Rectangle rectTotal = new Aspose.Pdf.Rectangle(100, 600, 200, 630);
            NumberField totalField = new NumberField(doc, rectTotal);
            totalField.PartialName = "Total";

            // JavaScript to compute the running total
            string js = "event.value = this.getField('Field1').value + this.getField('Field2').value;";
            totalField.Actions.OnCalculate = new JavascriptAction(js);
            form.Add(totalField);

            // Recalculate all calculated fields (Field.Recalculate recalculates the whole form)
            totalField.Recalculate();

            // Save the updated PDF (lifecycle rule: direct Save for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated field saved to '{outputPath}'.");
    }
}