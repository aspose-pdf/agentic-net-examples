using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Initialize FormEditor and bind the existing PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // JavaScript that validates the email format when the field loses focus (OnBlur)
            string emailValidationJs = @"
var email = event.target.value;
var pattern = /^[\w\.-]+@[\w\.-]+\.[A-Za-z]{2,}$/;
if (!pattern.test(email)) {
    app.alert('Invalid email address');
    event.rc = false; // prevent leaving the field
}
";

            // Attach the script to the field named "Email"
            formEditor.SetFieldScript("Email", emailValidationJs);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript added to field 'Email' and saved as '{outputPdf}'.");
    }
}