using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal.
        using (Document doc = new Document())
        {
            // Add a single page (default size).
            Page page = doc.Pages.Add();

            // Define a rectangle positioned in the top margin to act as the header.
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(
                0,                                 // left
                page.PageInfo.Height - 30,         // bottom (30 points from top edge)
                page.PageInfo.Width,               // right
                page.PageInfo.Height               // top
            );

            // Create a DateField that will display the current date.
            // Setting IsSharedField makes the field appear on every page.
            DateField dateField = new DateField(page, headerRect)
            {
                Name = "HeaderDate",
                IsSharedField = true,
                // Optional: set a default format for the date.
                DateFormat = "MM/dd/yyyy"
            };

            // Add the field to the document's form collection.
            doc.Form.Add(dateField);

            // Attach a JavaScript action that sets the field value to the current date
            // when the document is opened. The script runs in the PDF viewer.
            JavascriptAction jsAction = new JavascriptAction(
                "this.getField('HeaderDate').value = new Date().toLocaleDateString();"
            );
            // Assign the script to the document-level OpenAction.
            doc.OpenAction = jsAction;

            // Save the PDF. The Save call is inside the using block as required.
            doc.Save("HeaderWithCurrentDate.pdf");
        }

        Console.WriteLine("PDF created with a header displaying the current date.");
    }
}
