using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor facade and bind the source PDF
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // Retrieve the underlying Document object
            Document doc = editor.Document;

            // Access the form collection from the document (fully qualified to avoid ambiguity)
            Aspose.Pdf.Forms.Form pdfForm = doc.Form;

            // Retrieve the field named "Email" and cast it to Field
            Field emailField = pdfForm["Email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("Field 'Email' not found or is not a form field.");
                return;
            }

            // JavaScript to validate email format on blur (loss of focus)
            string jsCode = @"if (!/^[\w\.-]+@[\w\.-]+\.[a-zA-Z]{2,}$/.test(this.value)) {\n    app.alert('Invalid email address');\n    this.value = '';}";

            // OnLostFocus corresponds to the blur event for form fields
            emailField.Actions.OnLostFocus = new JavascriptAction(jsCode);

            // Save the modified PDF via the facade
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript validation: {outputPdf}");
    }
}
