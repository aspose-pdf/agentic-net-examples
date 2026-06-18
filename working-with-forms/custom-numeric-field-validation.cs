using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add a numeric field with custom range validation
        using (Document doc = new Document("input.pdf"))
        {
            // Define the rectangle where the field will be placed (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create the numeric field
            NumberField numberField = new NumberField(doc, fieldRect);
            numberField.PartialName = "Quantity";
            numberField.AlternateName = "Enter quantity (10‑100)";
            numberField.Required = true;

            // JavaScript validation: value must be between 10 and 100
            JavascriptAction validateAction = new JavascriptAction(
                "if (event.value < 10 || event.value > 100) {" +
                "app.alert('Value must be between 10 and 100');" +
                "event.rc = false;" +
                "}"
            );

            // Assign the validation action to the field
            numberField.Actions.OnValidate = validateAction;

            // Add the field to the first page (page indexing is 1‑based)
            doc.Form.Add(numberField, 1);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}