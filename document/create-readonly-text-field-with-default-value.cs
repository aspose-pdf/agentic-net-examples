using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_readonly.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field with a default value and read‑only state
            TextBoxField txtField = new TextBoxField(page, rect);
            txtField.PartialName = "MyTextField";
            txtField.Value = "Default text";   // default content
            txtField.ReadOnly = true;           // make the field read‑only

            // ---- Decoration (background, border, appearance) ----
            // Background colour (System.Drawing.Color)
            txtField.Characteristics.Background = System.Drawing.Color.LightGray;
            // Border colour (System.Drawing.Color)
            txtField.Characteristics.Border = System.Drawing.Color.Black;
            // Border width and style (requires Aspose.Pdf.Annotations.Border)
            txtField.Border = new Border(txtField) { Width = 1, Style = BorderStyle.Solid };
            // Text appearance (font, size, colour) – DefaultAppearance expects System.Drawing.Color
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
