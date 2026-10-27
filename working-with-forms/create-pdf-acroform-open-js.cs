using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithJS.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a text box field (AcroForm) on the page
            TextBoxField txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 300, 730));
            txtField.PartialName = "NameField";
            txtField.Value = "Enter name";
            doc.Form.Add(txtField);

            // Create a check box field (AcroForm) on the page
            CheckboxField chkField = new CheckboxField(page, new Aspose.Pdf.Rectangle(100, 650, 120, 670));
            chkField.PartialName = "AgreeCheck";
            chkField.Checked = false;
            doc.Form.Add(chkField);

            // Set a document‑level JavaScript that runs when the PDF is opened
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
