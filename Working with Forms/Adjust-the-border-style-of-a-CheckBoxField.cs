using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set a border for the checkbox.
            // Border requires the parent annotation (the checkbox) in its constructor.
            // Here we set a solid border with a width of 2 points.
            checkbox.Border = new Border(checkbox)
            {
                Style = BorderStyle.Solid,
                Width = 2
            };

            // Optionally set the checkbox appearance (checked state, export value, etc.)
            checkbox.Checked = false;
            checkbox.ExportValue = "Option1";

            // Add the checkbox to the form fields collection
            doc.Form.Add(checkbox);

            // Save the PDF document
            doc.Save("checkbox_with_border.pdf");
        }
    }
}