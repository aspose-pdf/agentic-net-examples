using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and convert it to PDF
        using (Document pdfDocument = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Add a document‑level JavaScript action (e.g., show an alert when the PDF is opened)
            // The JavaScript collection works like a dictionary – assign a script with a key.
            pdfDocument.JavaScript["DocOpen"] = "app.alert('Document opened');";

            // Example of adding a simple form field with a JavaScript validation action
            // Create a text box field on the first page
            TextBoxField txtField = new TextBoxField(pdfDocument.Pages[1], new Rectangle(100, 600, 300, 650));
            txtField.PartialName = "SampleField";
            txtField.Value = "";
            pdfDocument.Form.Add(txtField, 1);

            // Add a JavaScript action that validates the field when it loses focus (OnLostFocus event)
            string validationJs = "if (this.getField('SampleField').value == '') { app.alert('Field cannot be empty'); }";
            txtField.Actions.OnLostFocus = new JavascriptAction(validationJs);

            // Save the resulting PDF
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with JavaScript actions: {outputPdf}");
    }
}
