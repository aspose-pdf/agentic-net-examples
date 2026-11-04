using System;
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
            // Set default page margins (left, right, top, bottom) before adding any pages
            // MarginInfo is the correct type for page margins in Aspose.Pdf
            doc.PageInfo.Margin = new MarginInfo(50, 50, 50, 50); // 50 points ≈ 0.7 inch

            // Add a single page (the margins defined above will be applied automatically)
            Page page = doc.Pages.Add();

            // Define a rectangle for the text box field (coordinates are in points)
            var txtRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box form field
            var txtField = new TextBoxField(page, txtRect);
            txtField.PartialName = "CustomerName";
            txtField.Value = "John Doe";
            // Use AlternateName for tooltip/hover text (Tooltip property does not exist)
            txtField.AlternateName = "Enter customer name";
            // Add the text box to the document's form collection
            doc.Form.Add(txtField);

            // Define a rectangle for a checkbox field
            var chkRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);

            // Create a checkbox form field
            var chkField = new CheckboxField(page, chkRect);
            chkField.PartialName = "SubscribeNewsletter";
            chkField.Value = "Off"; // default unchecked
            // Use AlternateName for tooltip/hover text
            chkField.AlternateName = "Subscribe to newsletter?";
            // Add the checkbox to the document's form collection
            doc.Form.Add(chkField);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created successfully: {outputPath}");
    }
}
