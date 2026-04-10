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

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three pages (you can add as many as needed)
            for (int i = 0; i < 3; i++)
                doc.Pages.Add();

            // Define a shared text field for the header
            TextBoxField headerField = new TextBoxField(doc.Pages[1],
                new Aspose.Pdf.Rectangle(50, 800, 550, 830)); // position at top
            headerField.PartialName = "header";
            headerField.IsSharedField = true; // same field appears on every page
            doc.Form.Add(headerField);

            // Define a shared text field for the footer
            TextBoxField footerField = new TextBoxField(doc.Pages[1],
                new Aspose.Pdf.Rectangle(50, 30, 550, 60)); // position at bottom
            footerField.PartialName = "footer";
            footerField.IsSharedField = true;
            doc.Form.Add(footerField);

            // Place the shared fields on each page
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Header appearance
                doc.Form.AddFieldAppearance(headerField, pageNum,
                    new Aspose.Pdf.Rectangle(50, 800, 550, 830));

                // Footer appearance
                doc.Form.AddFieldAppearance(footerField, pageNum,
                    new Aspose.Pdf.Rectangle(50, 30, 550, 60));

                // JavaScript that runs when the page is opened
                string js = @"
                    var p = this.pageNum;
                    this.getField('header').value = 'Header - Page ' + p;
                    this.getField('footer').value = 'Footer - Page ' + p;
                ";
                doc.Pages[pageNum].Actions.OnOpen = new JavascriptAction(js);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic header/footer saved to '{outputPath}'.");
    }
}