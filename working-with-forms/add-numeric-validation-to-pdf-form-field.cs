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
        const string outputPath = "validated.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the numeric field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create a NumberField on the document (constructor with Document, Rectangle)
            NumberField numField = new NumberField(doc, rect)
            {
                // Internal name of the field
                PartialName = "Amount",
                // Allow digits and a decimal point
                AllowedChars = "0123456789.",
                // Optional: limit the number of characters the user can type
                MaxLen = 10
            };

            // Define the acceptable numeric range
            const double min = 0;
            const double max = 1000;

            // JavaScript that runs when the field value changes.
            // It parses the entered value and rejects it if it is outside the range.
            string js = $@"
                var val = parseFloat(event.value);
                if (isNaN(val) || val < {min} || val > {max}) {{
                    app.alert('Please enter a number between {min} and {max}.');
                    event.rc = false; // cancel the change
                }}
            ";

            // Assign the JavaScript to the field's OnValidate action
            numField.Actions.OnValidate = new JavascriptAction(js);

            // Add the field to the first page's annotation collection
            doc.Pages[1].Annotations.Add(numField);

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with numeric validation saved to '{outputPath}'.");
    }
}