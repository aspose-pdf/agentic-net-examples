using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithMargins.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define page margins (left, top, right, bottom) in points
            // Here we set 50 points (~0.7 inch) margin on each side
            page.PageInfo.Margin = new MarginInfo(50, 50, 50, 50);

            // Calculate the rectangle for the form field relative to the margins
            // Origin (0,0) is at the bottom‑left corner of the page.
            // We place the field 10 points inside the left and bottom margins.
            double left   = page.PageInfo.Margin.Left + 10;
            double bottom = page.PageInfo.Margin.Bottom + 10;
            double right  = left + 190;   // field width = 190 points
            double top    = bottom + 20;  // field height = 20 points

            // Create a rectangle that defines the field position and size
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(left, bottom, right, top);

            // Create a text box field on the page using the calculated rectangle
            TextBoxField textField = new TextBoxField(page, fieldRect);
            textField.PartialName = "CustomerName";   // internal field name
            textField.Value       = "John Doe";       // default value
            textField.Color       = Color.Black;       // border color
            // Removed BackgroundColor – not supported by TextBoxField

            // Set the border after the field has been instantiated (Border requires a parent reference)
            textField.Border = new Border(textField) { Width = 1 };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
