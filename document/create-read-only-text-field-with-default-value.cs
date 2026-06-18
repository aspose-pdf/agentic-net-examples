using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page where the field will be placed
            Page page = doc.Pages.Add();

            // Define the rectangle (llx, lly, urx, ury) for the text field
            // Fully qualified to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a TextBoxField on the specified page and rectangle
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                // Optional: give the field a name (used for form data extraction)
                PartialName = "SampleTextField",

                // Set the default value that appears when the PDF is opened
                Value = "Default Value"
            };

            // Set the default appearance (font, size, color) using the correct constructor
            // DefaultAppearance(string fontName, double fontSize, Color textColor)
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Make the field read‑only so the user cannot modify its content
            textField.ReadOnly = true;

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF to disk
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with read‑only text field created successfully.");
    }
}