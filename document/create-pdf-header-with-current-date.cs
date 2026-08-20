using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "HeaderDate.pdf";

        using (Document doc = new Document())
        {
            // Add the first page
            Page page = doc.Pages.Add();

            // Define the rectangle where the date will appear (top of the page)
            Rectangle dateRect = new Rectangle(
                50,                                 // left (lower‑left X)
                page.PageInfo.Height - 50,          // bottom (lower‑left Y)
                200,                                // right (upper‑right X)
                page.PageInfo.Height - 20);         // top (upper‑right Y)

            // Create a shared DateField that will be shown on every page
            DateField dateField = new DateField(page, dateRect);
            dateField.PartialName = "HeaderDate"; // give the field a known name
            dateField.IsSharedField = true;        // same field on all pages
            dateField.ReadOnly = true;             // user cannot edit
            doc.Form.Add(dateField);
            dateField.Init(page);

            // JavaScript that sets the field value to the current date when the page is opened
            string jsCode = "this.getField('HeaderDate').value = util.printd('mm/dd/yyyy', new Date());";
            page.Actions.OnOpen = new JavascriptAction(jsCode);

            // Add a second page to demonstrate that the header appears on all pages
            doc.Pages.Add();

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with header date saved to '{outputPath}'.");
    }
}
