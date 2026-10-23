using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "DynamicHeaderFooter.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Number of pages to generate
            const int pageCount = 3;

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page
                Page page = doc.Pages.Add();

                // ----- Header field -----
                // Create a text box form field for the header
                TextBoxField headerField = new TextBoxField(
                    page,
                    new Aspose.Pdf.Rectangle(
                        50,
                        page.PageInfo.Height - 50,
                        page.PageInfo.Width - 50,
                        page.PageInfo.Height - 30));
                headerField.PartialName = $"header{i}";
                headerField.ReadOnly = true; // make the field read‑only so users cannot edit it
                // Add the field to the document's AcroForm (not directly to page annotations)
                doc.Form.Add(headerField);

                // ----- Footer field -----
                TextBoxField footerField = new TextBoxField(
                    page,
                    new Aspose.Pdf.Rectangle(50, 30, page.PageInfo.Width - 50, 50));
                footerField.PartialName = $"footer{i}";
                footerField.ReadOnly = true;
                doc.Form.Add(footerField);
            }

            // JavaScript that runs when the document is opened.
            // It iterates over all pages and sets the header/footer field values
            // based on the current page number.
            string js = @"
var numPages = this.numPages;
for (var i = 0; i < numPages; i++) {
    var pageNum = i + 1;
    var hdr = this.getField('header' + pageNum);
    var ftr = this.getField('footer' + pageNum);
    if (hdr) hdr.value = 'Header - Page ' + pageNum;
    if (ftr) ftr.value = 'Footer - Page ' + pageNum;
}
";

            // Assign the JavaScript to the document's OpenAction
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF. The Document.Save(string) overload always writes PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic header/footer saved to '{outputPath}'.");
    }
}
