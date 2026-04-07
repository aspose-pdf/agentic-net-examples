using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = pdfDoc.Pages.Add();

            // Define the rectangle where the list box will appear
            // Parameters: lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y
            Aspose.Pdf.Rectangle listRect = new Aspose.Pdf.Rectangle(100, 600, 300, 500);

            // Create a ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, listRect)
            {
                // Allow multiple selections (optional for a dynamic list)
                MultiSelect = true,
                // Set a unique field name
                PartialName = "DynamicList"
            };

            // Add initial items to the list
            listBox.AddOption("Item 1");
            listBox.AddOption("Item 2");
            listBox.AddOption("Item 3");

            // Add the field to the form on page 1
            pdfDoc.Form.Add(listBox, 1);

            // Save the PDF document
            pdfDoc.Save("DynamicListForm.pdf");
        }
    }
}