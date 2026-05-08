using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the absolute position of the form field:
            // left = 100, bottom = 500, right = 300, top = 520
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a text box field placed at the specified rectangle
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "SampleField",   // internal field name
                Value       = "Hello, World!" // default content
            };

            // Set the default appearance (font, size, color) for the field
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Add the field to the form on page 1
            doc.Form.Add(textField, 1);

            // Add an additional appearance of the same field (optional, demonstrates AddFieldAppearance)
            doc.Form.AddFieldAppearance(textField, 1, fieldRect);

            // Save the PDF to disk
            doc.Save("AcroFormAbsolute.pdf");
        }

        Console.WriteLine("PDF with absolute‑positioned AcroForm fields created successfully.");
    }
}