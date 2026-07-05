using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string headerImagePath = "header.jpg";

        // ------------------------------------------------------------
        // Ensure a header image exists (create a simple one if missing)
        // ------------------------------------------------------------
        if (!System.IO.File.Exists(headerImagePath))
        {
            using (Bitmap bmp = new Bitmap(600, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.LightBlue);
                    g.DrawString(
                        "Header Image",
                        new System.Drawing.Font("Arial", 24),
                        System.Drawing.Brushes.DarkBlue,
                        new PointF(10, 30));
                }
                bmp.Save(headerImagePath);
            }
        }

        // ------------------------------------------------------------
        // Create a minimal PDF that contains a form field named "Header"
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Create a textbox form field positioned near the top of the page
            TextBoxField headerField = new TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(50, 700, 550, 750))
            {
                PartialName = "Header",
                Value = "Sample Header Text"
            };
            doc.Form.Add(headerField);
            doc.Save(inputPdfPath);
        }

        // ------------------------------------------------------------
        // Step 1: Add the background image as a header on every page
        // ------------------------------------------------------------
        PdfFileStamp stamp = new PdfFileStamp();               // non‑obsolete constructor
        stamp.BindPdf(inputPdfPath);                           // bind source PDF
        stamp.AddHeader(headerImagePath, 20f);                 // 20‑point top margin
        stamp.Save(outputPdfPath);                             // write result
        stamp.Close();                                         // release resources

        // ------------------------------------------------------------
        // Step 2: Decorate the "Header" form field with centered text
        // ------------------------------------------------------------
        FormEditor editor = new FormEditor();                  // non‑obsolete constructor
        editor.BindPdf(outputPdfPath);                         // work on the PDF produced above
        editor.Facade = new FormFieldFacade();
        editor.Facade.Alignment = FormFieldFacade.AlignCenter; // center horizontally
        editor.DecorateField("Header");
        editor.Save(outputPdfPath);                            // persist changes
    }
}
