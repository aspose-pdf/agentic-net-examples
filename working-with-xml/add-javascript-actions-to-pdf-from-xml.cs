using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xmlInputPath = "input.xml";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlInputPath}");
            return;
        }

        // Load XML and create a PDF document.
        using (Document pdfDoc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            // Add a text box form field.
            TextBoxField txtField = new TextBoxField(pdfDoc.Pages[1], new Rectangle(100, 600, 300, 630))
            {
                PartialName = "UserName",
                Value = ""
            };
            pdfDoc.Form.Add(txtField);

            // JavaScript for field validation.
            string jsCode = @"
if (this.getField('UserName').value == '') {
    app.alert('Please enter your name.');
}
";

            // Attach JavaScript to the OnLostFocus event (blur equivalent).
            txtField.Actions.OnLostFocus = new JavascriptAction(jsCode);

            // Document‑level JavaScript (executed when the PDF opens).
            string docLevelJs = @"
app.alert('Welcome to the interactive PDF!');
";
            pdfDoc.OpenAction = new JavascriptAction(docLevelJs);

            // Save the PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{pdfOutputPath}'.");
    }
}
