using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "field_listener.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field on the page
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "MyField",          // field identifier
                Value = ""                        // initial value (optional)
            };

            // Attach JavaScript that runs when the field value changes.
            // The OnValidate action is invoked after the user edits the field.
            textField.Actions.OnValidate = new JavascriptAction("app.alert('Field value changed');");

            // Add the field to the document's form
            doc.Form.Add(textField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with field listener saved to '{outputPath}'.");
    }
}