using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF classes
using Aspose.Pdf.Forms;          // Form field classes
using Aspose.Pdf.Annotations;    // For Rectangle (avoid ambiguity)

// This example adds a text box form field with a tooltip (AlternateName)
// that guides the user on the expected input format.

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Existing PDF
        const string outputPdf = "output_with_tooltip.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the first page
            TextBoxField txtField = new TextBoxField(doc, fieldRect);

            // Set a name for the field (used in form data)
            txtField.Name = "DateInput";

            // Set the tooltip that appears in PDF viewers (AlternateName)
            txtField.AlternateName = "Enter date in YYYY-MM-DD format";

            // Optionally set a default appearance (font, size, color)
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with tooltip: '{outputPdf}'");
    }
}