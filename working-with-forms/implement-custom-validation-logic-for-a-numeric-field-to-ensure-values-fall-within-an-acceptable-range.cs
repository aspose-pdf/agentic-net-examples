using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the numeric field will be placed
            // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);

            // Create a NumberField (inherits from TextBoxField) and add it to the document form
            NumberField numberField = new NumberField(doc, fieldRect)
            {
                // Field identifiers
                Name       = "Quantity",
                PartialName = "Quantity",

                // Allow only digits, optional decimal point and minus sign
                AllowedChars = "0123456789.-"
            };

            // Attach JavaScript validation that enforces the value to be within 10 and 100
            // The script runs when the user changes the field content.
            // event.rc = false cancels the change; app.alert shows a message.
            numberField.Actions.OnValidate = new JavascriptAction(
                "if (event.value < 10 || event.value > 100) {" +
                "    app.alert('Value must be between 10 and 100');" +
                "    event.rc = false;" +
                "}"
            );

            // Add the field to the document's AcroForm collection
            doc.Form.Add(numberField);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with numeric validation saved to '{outputPdf}'.");
    }
}