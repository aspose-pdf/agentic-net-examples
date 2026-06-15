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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Create a NumberField if it does not already exist
            NumberField numField;
            if (form["calcField"] == null)
            {
                // Define the field rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);
                numField = new NumberField(doc, rect);
                numField.PartialName = "calcField";
                form.Add(numField);
            }
            else
            {
                numField = form["calcField"] as NumberField;
            }

            // JavaScript to double the field value when it loses focus
            string js = "var v = this.getField('calcField').value;" +
                        "if (!isNaN(v)) { this.getField('calcField').value = (v * 2).toString(); }";

            // Assign the JavaScript action to the OnLostFocus event
            numField.Actions.OnLostFocus = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with JavaScript action to '{outputPath}'.");
    }
}