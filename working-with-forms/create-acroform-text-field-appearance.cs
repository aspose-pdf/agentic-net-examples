using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the form field will be placed
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the document (core API, not Facades)
            TextBoxField textField = new TextBoxField(doc, fieldRect);
            // Set the internal name of the field
            textField.PartialName = "SampleTextField";
            // Set the default appearance: font, size, and text color
            // DefaultAppearance(string fontName, double fontSize, System.Drawing.Color textColor)
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);
            // Set the outline color of the field (Aspose.Pdf.Color)
            textField.Color = Aspose.Pdf.Color.Red;
            // Optional: set an initial value
            textField.Value = "Enter text here";
            // Set the border (requires the parent annotation in the constructor)
            textField.Border = new Border(textField) { Width = 1 };

            // Add the field to the document's form on page 1
            // Overload Add(Field, int pageNumber) ensures the appearance is placed correctly
            doc.Form.Add(textField, 1);

            // Save the PDF to disk
            doc.Save("AcroFormWithAppearance.pdf");
        }

        Console.WriteLine("PDF with AcroForm created successfully.");
    }
}
