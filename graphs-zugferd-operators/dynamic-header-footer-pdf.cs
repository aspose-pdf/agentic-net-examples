using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Example: generate up to 4 pages (Aspose PDF evaluation mode limit)
            for (int i = 1; i <= 4; i++)
            {
                // Add a new page
                Page page = doc.Pages.Add();
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // ----- Header field (top of the page) -----
                // Rectangle: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
                var headerRect = new Aspose.Pdf.Rectangle(
                    0,                     // llx
                    pageHeight - 30,      // lly (30 units from top)
                    pageWidth,            // urx
                    pageHeight);          // ury (top edge)

                TextBoxField headerField = new TextBoxField(page, headerRect)
                {
                    Multiline = true,
                    ReadOnly = true,
                    PartialName = $"headerField{i}" // unique name per page
                };
                // Add the field to the form, specifying the 1‑based page number
                doc.Form.Add(headerField, i);

                // ----- Footer field (bottom of the page) -----
                var footerRect = new Aspose.Pdf.Rectangle(
                    0,   // llx
                    0,   // lly
                    pageWidth,
                    30); // ury (height 30 units)

                TextBoxField footerField = new TextBoxField(page, footerRect)
                {
                    Multiline = true,
                    ReadOnly = true,
                    PartialName = $"footerField{i}" // unique name per page
                };
                // Add the field to the form, specifying the 1‑based page number
                doc.Form.Add(footerField, i);

                // JavaScript that runs when the page is opened.
                // It sets the header and footer text based on the current page number.
                string js = $@"
                    var p = this.pageNum + 1; // pageNum is zero‑based
                    this.getField('headerField{i}').value = 'Header - Page ' + p;
                    this.getField('footerField{i}').value = 'Footer - Page ' + p;
                ";

                // Assign the script to the page's Open action (OnOpen property)
                page.Actions.OnOpen = new JavascriptAction(js);
            }

            // Save the PDF
            doc.Save("DynamicHeaderFooter.pdf");
        }

        Console.WriteLine("PDF with dynamic header/footer created successfully.");
    }
}