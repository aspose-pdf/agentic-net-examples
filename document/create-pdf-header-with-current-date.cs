using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "header_date.pdf";

        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define a rectangle for the header (top 50 points of the page)
            double pageHeight = page.PageInfo.Height;
            double pageWidth = page.PageInfo.Width;
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(0, pageHeight - 50, pageWidth, pageHeight);

            // Create a read‑only text box field that will display the date
            TextBoxField dateField = new TextBoxField(page, headerRect);
            dateField.PartialName = "dateField";
            dateField.ReadOnly = true;

            // Set appearance (font, size, colour)
            dateField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Align text to the right side of the field
            dateField.TextHorizontalAlignment = HorizontalAlignment.Right;

            // Remove visible border (width 0)
            dateField.Border = new Border(dateField) { Width = 0 };

            // Add the field to the form – the second argument is the 1‑based page number
            doc.Form.Add(dateField, 1);

            // JavaScript executed on document open to set the current date
            doc.OpenAction = new JavascriptAction("this.getField('dateField').value = util.printd('mm/dd/yyyy', new Date());");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date header saved to '{outputPath}'.");
    }
}
