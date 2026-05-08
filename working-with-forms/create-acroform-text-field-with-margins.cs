using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithMargins.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page with specific margins (left, right, top, bottom)
            Page page = doc.Pages.Add();
            page.PageInfo.Margin = new MarginInfo(50, 50, 50, 50); // 50 points margin on all sides

            // Define a rectangle for the text field relative to the page margins
            // Position: 20 points right of left margin, 30 points above bottom margin
            double fieldLeft   = page.PageInfo.Margin.Left + 20;
            double fieldBottom = page.PageInfo.Margin.Bottom + 30;
            double fieldWidth  = 200;
            double fieldHeight = 20;
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(
                fieldLeft,
                fieldBottom,
                fieldLeft + fieldWidth,
                fieldBottom + fieldHeight);

            // Create a text box field on the document using the rectangle
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                Name = "SampleTextField",
                Value = "Enter text here",
                // Set default appearance (font, size, color) using the correct constructor
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the form on page 1
            doc.Form.Add(textField, 1);

            // Optionally add another field using AddFieldAppearance for a second appearance
            // (e.g., same field on a different location)
            double secondLeft   = page.PageInfo.Margin.Left + 20;
            double secondBottom = fieldBottom + 50; // 50 points above the first field
            Aspose.Pdf.Rectangle secondRect = new Aspose.Pdf.Rectangle(
                secondLeft,
                secondBottom,
                secondLeft + fieldWidth,
                secondBottom + fieldHeight);
            doc.Form.AddFieldAppearance(textField, 1, secondRect);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}