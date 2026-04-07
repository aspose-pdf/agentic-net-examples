using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Add a push‑button field named "CalcButton" on page 1
                // Rectangle coordinates: llx, lly, urx, ury (in points)
                editor.AddField(FieldType.PushButton, "CalcButton", 1,
                    100f, 500f, 200f, 540f);

                // JavaScript that sums the values of fields "num1" and "num2"
                // and writes the result into the field "total"
                string js = @"
var f1 = this.getField('num1');
var f2 = this.getField('num2');
var totalField = this.getField('total');
if (f1 && f2 && totalField) {
    var sum = parseFloat(f1.value) + parseFloat(f2.value);
    totalField.value = sum;
}
";

                // Attach the script to the button
                editor.SetFieldScript("CalcButton", js);

                // Save the form changes back to the document
                editor.Save();
            }

            // Persist the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with calculation button saved to '{outputPdf}'.");
    }
}