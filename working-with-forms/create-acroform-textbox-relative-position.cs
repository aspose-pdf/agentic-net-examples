using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "AcroFormWithRelativePosition.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page with explicit size and margins
            Page page = doc.Pages.Add();
            // Set page size (A4) and margins (1 inch ≈ 72 points)
            page.PageInfo = new PageInfo
            {
                Width = 595,   // 8.27 inches
                Height = 842,  // 11.69 inches
                Margin = new MarginInfo
                {
                    Left = 72,
                    Right = 72,
                    Top = 72,
                    Bottom = 72
                }
            };

            // Retrieve margin values for relative positioning
            double marginLeft   = page.PageInfo.Margin.Left;
            double marginBottom = page.PageInfo.Margin.Bottom;

            // Define field size
            double fieldWidth  = 200;
            double fieldHeight = 20;

            // Compute field rectangle relative to margins (e.g., 50pt right of left margin,
            // 100pt above bottom margin)
            double fieldLeft   = marginLeft + 50;
            double fieldBottom = marginBottom + 100;
            double fieldRight  = fieldLeft + fieldWidth;
            double fieldTop    = fieldBottom + fieldHeight;

            // Fully qualified Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(fieldLeft, fieldBottom, fieldRight, fieldTop);

            // Create a text box field (AcroForm) and place it on the page
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                Name = "SampleTextBox",
                PartialName = "SampleTextBox",
                // Optional: set default appearance (font, size, color)
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the form on page 1
            doc.Form.Add(textField, 1);

            // Alternatively, add an additional appearance (same rectangle) if needed
            // doc.Form.AddFieldAppearance(textField, 1, fieldRect);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm field saved to '{outputPath}'.");
    }
}