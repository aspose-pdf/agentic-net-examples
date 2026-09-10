using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF to modify
        const string outputPdf = "output_validated.pdf"; // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the numeric field will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a NumberField on the first page of the document
            NumberField numberField = new NumberField(doc, fieldRect)
            {
                // Internal name of the field
                PartialName = "AmountField",

                // Allow digits, decimal point and minus sign
                AllowedChars = "0123456789.-"
            };

            // JavaScript that validates the entered value.
            // It checks that the value is between 0 and 1000.
            // If the check fails, an alert is shown and the entry is rejected.
            JavascriptAction validateAction = new JavascriptAction(
                "if (event.value < 0 || event.value > 1000) {" +
                "   app.alert('Please enter a value between 0 and 1000');" +
                "   event.rc = false;" +
                "}"
            );

            // Attach the validation script to the field's OnValidate action
            numberField.Actions.OnValidate = validateAction;

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with validated numeric field saved to '{outputPdf}'.");
    }
}