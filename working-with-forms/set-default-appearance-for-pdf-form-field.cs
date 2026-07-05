using System;
using System.Drawing; // required for Color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // optional, kept for completeness

class SetFieldDefaultAppearance
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the form field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the page
            TextBoxField textField = new TextBoxField(page, fieldRect);

            // Set the default appearance (font, size, color) using System.Drawing.Color
            textField.DefaultAppearance = new DefaultAppearance(
                "Helvetica",          // font name
                12,                    // font size
                System.Drawing.Color.Blue); // text color (System.Drawing.Color required)

            // Optionally set a name and initial value for the field
            textField.PartialName = "SampleField";
            textField.Value = "Default text";

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("field_with_default_appearance.pdf");
        }

        Console.WriteLine("PDF created with a form field that has a custom DefaultAppearance.");
    }
}