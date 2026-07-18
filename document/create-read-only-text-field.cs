using System;
using System.Drawing;                     // Required for DefaultAppearance constructor
using Aspose.Pdf;
using Aspose.Pdf.Annotations;            // DefaultAppearance class
using Aspose.Pdf.Forms;                  // TextBoxField class

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the specific page
            TextBoxField textField = new TextBoxField(page, fieldRect);

            // Add the field to the document's form collection (AcroForm)
            doc.Form.Add(textField);

            // Set the default value that appears in the field
            textField.Value = "Default Text";

            // Make the field read‑only
            textField.ReadOnly = true;

            // Define the default appearance (font, size, color) of the field
            // The constructor expects System.Drawing.Color as the third argument
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with read‑only text field created successfully.");
    }
}
