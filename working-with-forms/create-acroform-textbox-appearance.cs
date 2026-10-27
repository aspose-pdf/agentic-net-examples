using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (llx, lly, urx, ury)
            var fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a TextBoxField (AcroForm field) on the page
            var txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",          // Field name
                Value = "Enter text here"             // Default value
            };

            // Set appearance using DefaultAppearance object (font, size, text colour)
            // DefaultAppearance expects a System.Drawing.Color, not Aspose.Pdf.Color
            txtField.DefaultAppearance = new DefaultAppearance("Helv", 12, System.Drawing.Color.Blue);

            // Set border colour and width using the Color and Border properties (BorderColor does not exist)
            txtField.Color = Aspose.Pdf.Color.Black;               // border colour
            txtField.Border = new Border(txtField) { Width = 1 };   // optional border width

            // Set a light‑gray background if the property is available in the used version.
            // Older versions of Aspose.Pdf do not expose BackgroundColor on TextBoxField, so the line is commented out.
            // txtField.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9);

            // Add the field to the document's form (field index 1)
            doc.Form.Add(txtField, 1);

            // Save the PDF; Document.Save(string) writes a PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}
