using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "right_aligned_field.pdf";

        // Properly dispose the Document
        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Define the rectangle for the numeric field
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create the NumberField and configure it
            NumberField numberField = new NumberField(page, fieldRect)
            {
                TextHorizontalAlignment = HorizontalAlignment.Right,
                AllowedChars = "0123456789"
            };

            // Add the field to the document's Form collection (not to the Page)
            doc.Form.Add(numberField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
