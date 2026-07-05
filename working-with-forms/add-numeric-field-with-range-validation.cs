using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        // Paths for the generated PDF
        const string outputPath = "numeric_field_with_range_validation.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the number field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a NumberField on the page
            NumberField numberField = new NumberField(page, fieldRect)
            {
                // Allow only digits (default is already "0123456789")
                AllowedChars = "0123456789",
                // Optional: limit the number of characters the user can type
                MaxLen = 5,
                // Set a tooltip (alternate name) for better UX
                AlternateName = "Enter a number between 10 and 100"
            };

            // Add the field to the document's form collection (not to the page)
            doc.Form.Add(numberField);

            // Define the acceptable range (inclusive)
            const int minValue = 10;
            const int maxValue = 100;

            // Attach a JavaScript validation action that runs when the user changes the field's value
            // The script checks the value and, if out of range, shows an alert and cancels the change
            string js = $@"
                var val = parseInt(event.value);
                if (isNaN(val) || val < {minValue} || val > {maxValue}) {{
                    app.alert('Please enter a number between {minValue} and {maxValue}.');
                    event.rc = false; // reject the entered value
                }}
            ";

            numberField.Actions.OnValidate = new JavascriptAction(js);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with numeric field saved to '{outputPath}'.");
    }
}
