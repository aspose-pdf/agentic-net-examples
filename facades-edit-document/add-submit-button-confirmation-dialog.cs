using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "form_input.pdf";
        const string outputPdf = "form_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // JavaScript that shows a confirmation dialog before submitting the form.
        // If the user clicks "Yes", the form is submitted; otherwise the submission is cancelled.
        string js = @"var response = app.alert({
                cMsg: 'Do you want to submit the form?',
                nIcon: 2,               // Question icon
                nType: 2                // Yes/No buttons
            });
            if (response == 4) {      // 4 = Yes
                this.submitForm();    // Adjust parameters as needed (e.g., URL, format)
            }";

        // Load the existing PDF document.
        Document doc = new Document(inputPdf);

        // Attach the JavaScript to be executed when the document is opened.
        doc.OpenAction = new JavascriptAction(js);

        // Save the modified PDF.
        doc.Save(outputPdf);

        Console.WriteLine($"JavaScript added and PDF saved to '{outputPdf}'.");
    }
}