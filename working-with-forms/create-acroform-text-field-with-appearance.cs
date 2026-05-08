using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the document
            TextBoxField txtField = new TextBoxField(doc, fieldRect);
            txtField.PartialName = "SampleField";
            txtField.Value = "Enter text";

            // Set the default appearance (font, size, color) for the field
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // Optionally add an additional appearance (same rectangle)
            doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}