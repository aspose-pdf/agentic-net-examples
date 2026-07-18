using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF that already contains the fields "Item1" and "Item2"
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Create a hidden calculation field (zero‑size rectangle)
            // Using NumberField because it stores numeric values
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            NumberField totalField = new NumberField(doc, hiddenRect)
            {
                PartialName = "Total",   // field name
                ReadOnly    = true       // prevent user editing
            };

            // Add the field to page 1 (page indexing is 1‑based)
            form.Add(totalField, 1);

            // JavaScript that sums the values of Item1 and Item2
            // The script assigns the result to the current field (event.value)
            JavascriptAction calcJs = new JavascriptAction(
                "event.value = this.getField('Item1').value + this.getField('Item2').value;");

            // Attach the JavaScript to the OnCalculate action of the field
            totalField.Actions.OnCalculate = calcJs;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden calculation field created and saved to '{outputPath}'.");
    }
}