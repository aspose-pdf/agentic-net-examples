using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;      // JavascriptAction
using Aspose.Pdf.Forms;           // DateField
using Aspose.Pdf.Text;            // FontRepository (if needed)

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "HeaderWithDate.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define a rectangle for the date field placed in the header area
            // (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            Aspose.Pdf.Rectangle dateRect = new Aspose.Pdf.Rectangle(0, pageHeight - 50, pageWidth, pageHeight);

            // Create a shared DateField that will appear on every page (header)
            DateField dateField = new DateField(page, dateRect)
            {
                PartialName   = "dateHeader",   // field name used in JavaScript
                IsSharedField = true            // same field instance on all pages
            };
            doc.Form.Add(dateField);

            // Attach JavaScript to the page Open action.
            // The script sets the field value to the current date when the page is opened.
            string js = @"
                var f = this.getField('dateHeader');
                if (f) {
                    // Format: MM/DD/YYYY
                    f.value = util.printd('mm/dd/yyyy', new Date());
                }
            ";
            // Correct property for page‑level JavaScript is OnOpen
            page.Actions.OnOpen = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with header date saved to '{outputPath}'.");
    }
}
