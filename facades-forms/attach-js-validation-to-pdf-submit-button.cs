using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // needed for FormEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF containing a submit button named "SubmitBtn"
        const string outputPath = "output.pdf";         // Resulting PDF with validation JavaScript
        const string submitUrl  = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to guarantee disposal (rule: document-disposal-with-using)
        using (Document pdfDoc = new Document(inputPath))
        {
            // JavaScript that checks required fields before submitting.
            // Adjust field names ("Name", "Email") to match the actual form field names in the PDF.
            string validationJs = $@"
if (this.getField('Name').value == '' || this.getField('Email').value == '') {{
    app.alert('Please fill in all required fields before submitting.');
}} else {{
    this.submitForm({{cURL: '{submitUrl}'}});
}}";

            // Use FormEditor to attach the JavaScript to the submit button field.
            // This avoids the non‑existent Aspose.Pdf.Actions namespace (rule: use-formeditor-setfieldscript-for-field-javascript).
            FormEditor editor = new FormEditor();
            editor.BindPdf(pdfDoc);
            editor.SetFieldScript("SubmitBtn", validationJs);
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"PDF with validation JavaScript saved to '{outputPath}'.");
    }
}
