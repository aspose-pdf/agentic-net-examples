using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // existing PDF to host the form
        const string outputPath = "output.pdf";     // result with validation
        const double minValue   = 10;               // lower bound
        const double maxValue   = 100;              // upper bound

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the numeric field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);

            // Create a NumberField on the document (adds it to the first page automatically)
            NumberField numberField = new NumberField(doc, fieldRect);
            numberField.Name        = "Amount";
            numberField.PartialName = "Amount";

            // Restrict characters to digits and a decimal point
            numberField.AllowedChars = "0123456789.";

            // Set visual appearance (font, size, color)
            numberField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // JavaScript that validates the entered value against the defined range
            string jsCode = $"if (event.value == '' || isNaN(event.value) || event.value < {minValue} || event.value > {maxValue}) {{ app.alert('Please enter a number between {minValue} and {maxValue}.'); event.rc = false; }}";

            // Assign the JavaScript to the OnValidate action of the field
            JavascriptAction validateAction = new JavascriptAction(jsCode);
            numberField.Actions.OnValidate = validateAction;

            // Add the field to the page's annotation collection
            doc.Pages[1].Annotations.Add(numberField);

            // Save the modified PDF (PDF output, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Validated PDF saved to '{outputPath}'.");
    }
}