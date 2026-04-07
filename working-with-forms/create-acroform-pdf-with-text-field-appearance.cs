using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Document must be disposed deterministically
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will appear
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a DefaultAppearance with font, size and color
            // (DefaultAppearance.Font is read‑only, so use the constructor)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Create a text box field and assign appearance properties
            TextBoxField textField = new TextBoxField(doc, fieldRect);
            textField.Name = "SampleField";
            textField.PartialName = "SampleField";
            textField.DefaultAppearance = appearance;               // font & color
            textField.Color = Aspose.Pdf.Color.LightGray;           // border color (optional)

            // Add the field to the form on page 1
            doc.Form.Add(textField, 1);

            // Add an additional appearance (required for some viewers)
            doc.Form.AddFieldAppearance(textField, 1, fieldRect);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}