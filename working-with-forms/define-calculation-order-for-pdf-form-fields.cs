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
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure automatic recalculation is enabled (default is true)
            doc.Form.AutoRecalculate = true;

            // Retrieve the fields that participate in the calculation.
            // Replace "Item1", "Item2", "Total" with actual field names in your PDF.
            Field item1Field = (Field)doc.Form["Item1"];
            Field item2Field = (Field)doc.Form["Item2"];
            Field totalField = (Field)doc.Form["Total"];

            // Define the calculation order: first calculate Item1 and Item2,
            // then calculate Total which depends on the previous fields.
            doc.Form.CalculatedFields = new List<Field>
            {
                item1Field,
                item2Field,
                totalField
            };

            // Optionally, assign JavaScript to the Total field to sum the values.
            // This step is not required for order definition but demonstrates a typical use case.
            totalField.Actions.OnCalculate = new JavascriptAction(
                "event.value = this.getField('Item1').value + this.getField('Item2').value;"
            );

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with calculation order: {outputPath}");
    }
}
