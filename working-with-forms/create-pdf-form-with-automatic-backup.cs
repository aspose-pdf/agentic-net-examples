using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPdf = "form_with_backup.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Form pdfForm = doc.Form;

            TextBoxField txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 630))
            {
                PartialName = "SampleText",
                Value = "Enter text here"
            };

            pdfForm.Add(txtField, 1);

            // Correct property for a blur (loss of focus) event
            txtField.Actions.OnLostFocus = new JavascriptAction("this.saveAs('backup.pdf');");

            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF form created: {outputPdf}");
        Console.WriteLine("When the text field loses focus, the viewer will attempt to save a backup copy named 'backup.pdf'.");
    }
}
