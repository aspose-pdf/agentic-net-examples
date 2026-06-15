using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormAbsolute.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define absolute position rectangle (llx, lly, urx, ury)
            // Example: field placed at (100, 600) with width 200 and height 30
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field (AcroForm field)
            TextBoxField txtField = new TextBoxField(doc);
            txtField.PartialName = "CustomerName";
            txtField.Value = "John Doe";

            // Set the field's rectangle to the absolute position defined above
            txtField.Rect = fieldRect;

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // Optional: set default appearance (font, size, color) for the form
            // Note: DefaultAppearance constructor requires System.Drawing.Color for the text color
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with absolute positioned AcroForm field saved to '{outputPath}'.");
    }
}
