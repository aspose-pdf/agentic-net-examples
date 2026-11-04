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

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the form field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field and assign a partial name
            TextBoxField txtField = new TextBoxField(page, fieldRect);
            txtField.PartialName = "SampleField";
            txtField.Value = "Enter text";

            // Set appearance properties via DefaultAppearance (lives in Aspose.Pdf.Annotations)
            // Constructor: (string fontName, double fontSize, System.Drawing.Color foregroundColor)
            txtField.DefaultAppearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Blue);

            // Set border and background colors via Characteristics (also System.Drawing.Color)
            txtField.Characteristics.Border = System.Drawing.Color.Black;
            txtField.Characteristics.Background = System.Drawing.Color.LightGray;

            // Add the field to the document's AcroForm
            doc.Form.Add(txtField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}