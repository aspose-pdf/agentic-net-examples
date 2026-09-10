using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the form fields
            Page page = doc.Pages.Add();

            // ---------- Create required text fields ----------
            // Name field (required)
            TextBoxField nameField = new TextBoxField(page, new Rectangle(100, 700, 300, 730))
            {
                PartialName = "Name",
                Required = true,
                Contents = "Enter name"
            };
            doc.Form.Add(nameField);

            // Email field (required)
            TextBoxField emailField = new TextBoxField(page, new Rectangle(100, 650, 300, 680))
            {
                PartialName = "Email",
                Required = true,
                Contents = "Enter email"
            };
            doc.Form.Add(emailField);

            // ---------- Create submit button ----------
            ButtonField submitBtn = new ButtonField(page, new Rectangle(100, 580, 200, 610))
            {
                PartialName = "SubmitBtn",
                Contents = "Submit"
            };
            doc.Form.Add(submitBtn);

            // JavaScript that validates required fields before submission
            string js = @"
var fields = this.getFieldNames();
for (var i = 0; i < fields.length; i++) {
    var f = this.getField(fields[i]);
    if (f.required && (f.value == null || f.value == '')) {
        app.alert('Please fill all required fields.');
        return false;
    }
}
this.submitForm({cURL:'https://example.com/submit', cSubmitAs:'HTML'});
";

            // Attach the JavaScript action to the button (use a valid action property)
            JavascriptAction jsAction = new JavascriptAction(js);
            // OnPressMouseBtn is a valid property of AnnotationActionCollection for button clicks
            submitBtn.Actions.OnPressMouseBtn = jsAction;

            // Save the PDF
            doc.Save("FormWithValidation.pdf");
        }

        Console.WriteLine("PDF created with required-field validation on the submit button.");
    }
}