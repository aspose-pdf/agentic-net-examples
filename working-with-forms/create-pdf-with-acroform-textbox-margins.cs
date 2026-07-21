using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_margins.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Set page margins (values are in points)
            page.PageInfo.Margin = new MarginInfo
            {
                Top = 50,
                Bottom = 50,
                Left = 50,
                Right = 50
            };

            // Define the rectangle where the text box field will be placed
            // Coordinates are measured from the lower‑left corner of the page
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field and configure basic properties
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "NameField",
                Value = "Enter name"
            };

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}