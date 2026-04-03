using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithMargins.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Set uniform page margins (values are in points)
            page.PageInfo.Margin = new MarginInfo
            {
                Top    = 50,
                Bottom = 50,
                Left   = 50,
                Right  = 50
            };

            // Define the rectangle for the form field (llx, lly, urx, ury)
            // Placed within the margins to keep layout consistent
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a text box field bound to the document
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "CustomerName",   // field identifier
                Value       = "Enter name"      // default placeholder text
            };

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}