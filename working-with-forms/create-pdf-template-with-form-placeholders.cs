using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for DefaultAppearance
using System.Drawing; // for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string outputPath = "template.pdf";

        // Create an empty PDF document using a DocumentFactory instance
        DocumentFactory factory = new DocumentFactory();
        using (Document doc = factory.CreateDocument())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // ---------- Text box placeholder ----------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField txtField = new TextBoxField(page, txtRect)
            {
                PartialName = "CustomerName",
                Value = "<<CustomerName>>",
                // DefaultAppearance expects System.Drawing.Color
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };
            doc.Form.Add(txtField);

            // ---------- Date field placeholder ----------
            Aspose.Pdf.Rectangle dateRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);
            TextBoxField dateField = new TextBoxField(page, dateRect)
            {
                PartialName = "InvoiceDate",
                Value = "<<InvoiceDate>>",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };
            doc.Form.Add(dateField);

            // ---------- Checkbox placeholder ----------
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 400, 120, 420);
            CheckboxField chkField = new CheckboxField(page, chkRect)
            {
                PartialName = "IsPaid",
                ExportValue = "Yes",   // value when checked
                Value = "Off"          // default unchecked state (placeholder)
            };
            doc.Form.Add(chkField);

            // Save the PDF template
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF template created at '{outputPath}'.");
    }
}
