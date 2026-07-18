using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the XML and convert it to a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Document‑level JavaScript executed when the PDF is opened
            doc.OpenAction = new JavascriptAction("app.alert('PDF opened');");

            // Example: add validation JavaScript to a text box field named "NameField"
            TextBoxField txtField = doc.Form["NameField"] as TextBoxField;
            if (txtField != null)
            {
                // Validation: require at least 3 characters
                txtField.Actions.OnValidate = new JavascriptAction(
                    "if (event.value.length < 3) { " +
                    "app.alert('Name must be at least 3 characters'); " +
                    "event.rc = false; }"
                );
            }

            // Save the PDF with the embedded JavaScript actions
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPdf}'.");
    }
}
