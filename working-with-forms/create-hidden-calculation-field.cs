using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithCalculation.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a single blank page.
            Page page = doc.Pages.Add();

            // ----- Create visible input fields: Item1 and Item2 -----
            // Position and size are arbitrary for demonstration.
            TextBoxField item1 = new TextBoxField(page, new Rectangle(100, 700, 250, 720))
            {
                PartialName = "Item1",
                Value = "0"
            };
            doc.Form.Add(item1);

            TextBoxField item2 = new TextBoxField(page, new Rectangle(100, 650, 250, 670))
            {
                PartialName = "Item2",
                Value = "0"
            };
            doc.Form.Add(item2);

            // ----- Create hidden calculation field -----
            // The field is not visible in the PDF viewer.
            TextBoxField sumField = new TextBoxField(page, new Rectangle(0, 0, 0, 0))
            {
                PartialName = "Sum",
                ReadOnly = true,          // User cannot edit the result.
                Flags = AnnotationFlags.Hidden // Hide the field.
            };
            doc.Form.Add(sumField);

            // Attach JavaScript that runs when the field needs to be calculated.
            // The script reads the values of Item1 and Item2, converts them to numbers,
            // adds them, and assigns the result to the current field (event.value).
            sumField.Actions.OnCalculate = new JavascriptAction(
                "var v1 = parseFloat(this.getField('Item1').value) || 0;" +
                "var v2 = parseFloat(this.getField('Item2').value) || 0;" +
                "event.value = (v1 + v2).toString();"
            );

            // Save the PDF. No SaveOptions are required because we are saving to PDF format.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden calculation field saved to '{outputPath}'.");
    }
}
