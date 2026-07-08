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
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access the form object (fully qualified to avoid ambiguity)
            Form form = doc.Form;

            // Ensure automatic recalculation is enabled (default is true)
            form.AutoRecalculate = true;

            // Retrieve fields by their full names – the Form indexer returns a WidgetAnnotation,
            // so we must cast it to Aspose.Pdf.Forms.Field (or a derived field type).
            Field qtyField   = doc.Form["Quantity"] as Field;
            Field priceField = doc.Form["UnitPrice"] as Field;
            Field totalField = doc.Form["Total"] as Field;

            // Validate that the fields were found and are of the correct type.
            if (qtyField == null || priceField == null || totalField == null)
            {
                Console.Error.WriteLine("One or more required form fields were not found or are not form fields.");
                return;
            }

            // Define a JavaScript calculation for the Total field
            JavascriptAction calcScript = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('UnitPrice').value;");
            totalField.Actions.OnCalculate = calcScript;

            // Define the order in which calculated fields are evaluated.
            form.CalculatedFields = new List<Field>
            {
                qtyField,   // first: input fields (no calculation)
                priceField, // second: input fields (no calculation)
                totalField  // third: dependent field, calculated after inputs
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form with calculation order saved to '{outputPath}'.");
    }
}
