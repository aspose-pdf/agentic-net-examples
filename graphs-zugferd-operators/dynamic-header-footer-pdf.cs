using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "dynamic_header_footer.pdf";

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add three sample pages
            for (int i = 0; i < 3; i++)
                doc.Pages.Add();

            // Define header and footer rectangles (left, bottom, width, height)
            // Note: Aspose.Pdf.Rectangle constructor uses (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(0, 800, 595, 820);
            Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(0, 20, 595, 40);

            // JavaScript that updates header and footer on each page open
            string js = @"
var hdr = this.getField('header');
var ftr = this.getField('footer');
hdr.value = 'Header - Page ' + this.pageNum + ' of ' + this.numPages;
ftr.value = 'Footer - Page ' + this.pageNum + ' of ' + this.numPages;
";

            // Create header/footer fields on every page
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                // Header field – use the constructor that takes a Rectangle, then set the field name via PartialName
                TextBoxField headerField = new TextBoxField(doc.Pages[p], headerRect);
                headerField.PartialName = "header";
                headerField.ReadOnly = true;
                headerField.Border = new Border(headerField) { Width = 0 };
                headerField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
                headerField.TextHorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                doc.Form.Add(headerField, p);

                // Footer field – same approach as header
                TextBoxField footerField = new TextBoxField(doc.Pages[p], footerRect);
                footerField.PartialName = "footer";
                footerField.ReadOnly = true;
                footerField.Border = new Border(footerField) { Width = 0 };
                footerField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
                footerField.TextHorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                doc.Form.Add(footerField, p);

                // Attach JavaScript to the Open action of the page
                doc.Pages[p].Actions.OnOpen = new JavascriptAction(js);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic header/footer saved to '{outputPath}'.");
    }
}
