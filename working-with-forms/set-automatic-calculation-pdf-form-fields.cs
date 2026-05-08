using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;
            // Enable automatic recalculation of calculated fields
            form.AutoRecalculate = true;

            // Retrieve fields by name – the indexer returns a WidgetAnnotation, so cast to Field
            Field qtyField   = (Field)form["Qty"];
            Field priceField = (Field)form["Price"];
            Field totalField = (Field)form["Total"];

            // JavaScript that calculates Total = Qty * Price
            string js = "event.value = this.getField('Qty').value * this.getField('Price').value;";

            // Assign the calculation script to the Total field
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // No explicit calculation order is required – AutoRecalculate will handle it.
            // The Form class does not expose a public getter for CalculatedFields, and there is no
            // Form.Calculate() method in the current Aspose.Pdf API, so those calls are omitted.

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
