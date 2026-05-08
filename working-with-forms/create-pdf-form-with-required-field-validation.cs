using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ---------- Required Text Fields ----------
            // Name field (required)
            TextBoxField nameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 300, 720));
            nameField.PartialName = "Name";
            nameField.Required = true;               // Mark as required
            nameField.Contents = "Enter Name";
            doc.Form.Add(nameField);

            // Email field (required)
            TextBoxField emailField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 300, 670));
            emailField.PartialName = "Email";
            emailField.Required = true;              // Mark as required
            emailField.Contents = "Enter Email";
            doc.Form.Add(emailField);

            // ---------- Submit Button ----------
            // Create a button that will submit the form
            ButtonField submitBtn = new ButtonField(page, new Aspose.Pdf.Rectangle(100, 580, 200, 610));
            submitBtn.PartialName = "SubmitBtn";
            submitBtn.Contents = "Submit";

            // JavaScript that validates required fields before submitting
            string jsCode = @"
var name = this.getField('Name').value;
var email = this.getField('Email').value;
if (name !== '' && email !== '') {
    // All required fields are filled – submit the form
    this.submitForm({cURL:'https://example.com/submit'});
} else {
    // Show an alert if any required field is empty
    app.alert('Please fill all required fields before submitting.');
}";
            // Attach the JavaScript to the button's press mouse button action (valid property)
            submitBtn.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the form
            doc.Form.Add(submitBtn);

            // Save the PDF
            doc.Save("FormWithValidation.pdf");
        }

        Console.WriteLine("PDF created with required-field validation.");
    }
}
