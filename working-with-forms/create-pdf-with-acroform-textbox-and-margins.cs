using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_margins.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Define page margins (points)
            MarginInfo margins = new MarginInfo
            {
                Top = 50,
                Bottom = 50,
                Left = 50,
                Right = 50
            };

            // Add a new page and apply the margins
            Page page = doc.Pages.Add();
            page.PageInfo.Margin = margins;

            // Create a text box field positioned within the page margins
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "Name",
                Value = "Enter name"
            };

            // Add the field to the document's form
            doc.Form.Add(txtField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}