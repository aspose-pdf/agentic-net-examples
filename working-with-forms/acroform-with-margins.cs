using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithMargins.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page and define its margins (left, top, right, bottom)
            Page page = doc.Pages.Add();
            page.PageInfo.Margin = new MarginInfo(50, 50, 50, 50); // 50 points on each side

            // Compute field position relative to the page margins
            // Example: place the field 10 points right of the left margin and 20 points above the bottom margin
            double fieldLeft   = page.PageInfo.Margin.Left + 10;
            double fieldBottom = page.PageInfo.Margin.Bottom + 20;
            double fieldWidth  = 200;
            double fieldHeight = 25;
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(
                fieldLeft,
                fieldBottom,
                fieldLeft + fieldWidth,
                fieldBottom + fieldHeight);

            // Create a text box field on the page using the calculated rectangle
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                Name  = "SampleTextBox",
                Value = "Enter text here"
            };

            // Add the field to the document's form (page number is 1‑based)
            doc.Form.Add(textField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}