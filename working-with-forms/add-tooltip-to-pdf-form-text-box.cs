using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithTooltip.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the page
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                // Set the field name (used in form data)
                Name = "DateInput",

                // Set the tooltip that appears in Adobe Acrobat
                AlternateName = "Enter the date in MM/DD/YYYY format"
                // Optional: default appearance can be set via the string property if needed
                // DefaultAppearance = "/Helv 12 Tf 0 g" // example of raw PDF appearance string
            };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with tooltip saved to '{outputPath}'.");
    }
}
