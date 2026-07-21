using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (AcroForm)
            Form form = doc.Form;

            // Ensure automatic recalculation is enabled (default is true)
            form.AutoRecalculate = true;

            // Retrieve fields that participate in the calculation.
            // The indexer returns a WidgetAnnotation, so cast to Field.
            Field quantityField  = (Field)form["Quantity"];
            Field unitPriceField = (Field)form["UnitPrice"];
            Field totalField     = (Field)form["Total"];

            // Define the calculation order:
            // First calculate "Total" after "Quantity" and "UnitPrice" have values.
            // The order in the list determines the sequence of recalculation.
            form.CalculatedFields = new List<Field>
            {
                quantityField,
                unitPriceField,
                totalField
            };

            // Assign a JavaScript calculation to the "Total" field.
            // This script multiplies Quantity * UnitPrice.
            totalField.Actions.OnCalculate = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('UnitPrice').value;"
            );

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with calculation order: {outputPath}");
    }
}
