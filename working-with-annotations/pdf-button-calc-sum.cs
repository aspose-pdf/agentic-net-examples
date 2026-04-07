using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "sum_form.pdf";

        // Create a new PDF document with a single blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize FormEditor with the document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add two input text fields
                formEditor.AddField(FieldType.Text, "Field1", 1, 50, 750, 200, 770);
                formEditor.AddField(FieldType.Text, "Field2", 1, 250, 750, 400, 770);

                // Add a read‑only result field
                formEditor.AddField(FieldType.Text, "Result", 1, 450, 750, 600, 770);
                formEditor.SetFieldAttribute("Result", PropertyFlag.ReadOnly);

                // Add a push button
                formEditor.AddField(FieldType.PushButton, "CalcButton", 1, 250, 700, 350, 730);

                // Set the button caption
                if (doc.Form["CalcButton"] is ButtonField btn)
                {
                    btn.NormalCaption = "Calculate";
                }

                // JavaScript that sums the two fields and writes the result
                string js = @"
var f1 = this.getField('Field1').value;
var f2 = this.getField('Field2').value;
var sum = parseFloat(f1) + parseFloat(f2);
this.getField('Result').value = sum;
";

                // Attach the script to the button
                formEditor.SetFieldScript("CalcButton", js);

                // Save the PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with calculation button saved to '{outputPath}'.");
    }
}