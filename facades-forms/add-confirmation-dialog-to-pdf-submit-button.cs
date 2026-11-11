using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for JavascriptAction
using Aspose.Pdf.Forms;      // for ButtonField

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Locate the button named "SubmitForm"
            ButtonField submitButton = doc.Form["SubmitForm"] as ButtonField;
            if (submitButton == null)
            {
                Console.Error.WriteLine("Button field 'SubmitForm' not found.");
                return;
            }

            // JavaScript that shows a confirmation dialog before submitting the form
            string js = @"if (app.alert('Do you want to submit the form?', 3) == 4) {\n    this.submitForm();\n}";

            // Attach the JavaScript action to the button's click event
            submitButton.OnActivated = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPath}");
    }
}
