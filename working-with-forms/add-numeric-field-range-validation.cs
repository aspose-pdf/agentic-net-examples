using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "template.pdf";   // existing PDF or blank template
        const string outputPath = "validated_form.pdf";

        // Define acceptable numeric range
        const double minValue = 10.0;
        const double maxValue = 1000.0;

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using the required load options if needed)
            using (Document doc = new Document(inputPath))
            {
                // Create a NumberField on the first page
                // Fully qualify Rectangle to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
                NumberField numberField = new NumberField(doc, fieldRect);

                // Set a name for the field (used for form data extraction)
                numberField.Name = "Amount";

                // Restrict allowed characters to digits, decimal point and minus sign
                numberField.AllowedChars = "0123456789.-";

                // Optional: limit the maximum length of the input
                numberField.MaxLen = 12;

                // Set an initial value (optional)
                numberField.Value = "0";

                // Attach a JavaScript validation action that runs when the field loses focus
                // The script checks the numeric value against the defined range.
                // If the value is out of range, it shows an alert and cancels the change (event.rc = false).
                string js = $@"
                    var val = parseFloat(event.value);
                    if (isNaN(val) || val < {minValue} || val > {maxValue}) {{
                        app.alert('Please enter a number between {minValue} and {maxValue}.');
                        event.rc = false; // reject the change
                    }}
                ";
                numberField.Actions.OnValidate = new JavascriptAction(js);

                // Add the field to the page's annotations collection
                doc.Pages[1].Annotations.Add(numberField);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with numeric validation saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}