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
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the document
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "SampleTextBox",
                Contents    = "Enter text here"
            };

            // Set the field's default appearance (font, size, color)
            // Use the constructor that accepts font name, size and Aspose.Pdf.Color
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Optional visual styling: border width and border color
            txtField.Border = new Border(txtField) { Width = 1 };
            txtField.Color  = Aspose.Pdf.Color.Black; // border color

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}