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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create a required text box field (e.g., "Name")
            // -------------------------------------------------
            // Rectangle coordinates: llx, lly, urx, ury
            Aspose.Pdf.Rectangle nameFieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            // Use the constructor that receives the page instance
            TextBoxField nameField = new TextBoxField(page, nameFieldRect)
            {
                PartialName = "Name",   // field identifier
                Required = true,       // mark as required
                Contents = ""          // initial empty value
            };
            // Add the field to the form collection
            doc.Form.Add(nameField);

            // -------------------------------------------------
            // Create a submit button
            // -------------------------------------------------
            Aspose.Pdf.Rectangle submitBtnRect = new Aspose.Pdf.Rectangle(100, 600, 200, 630);
            // Use the constructor that receives the page instance
            ButtonField submitBtn = new ButtonField(page, submitBtnRect)
            {
                PartialName = "Submit",
                // The visible caption of the button
                Contents = "Submit"
            };

            // JavaScript that validates required fields before submitting
            // If any required field is empty, an alert is shown.
            // Otherwise, the form is submitted to the specified URL.
            string jsCode = @"
                // Retrieve the required field value
                var name = this.getField('Name').value;
                // Check if the field is empty
                if (name == null || name == '')
                {
                    // Show an alert and prevent submission
                    app.alert('Please fill in all required fields.');
                }
                else
                {
                    // All required fields are filled – submit the form
                    this.submitForm({cURL:'https://example.com/submit'});
                }
            ";

            // Attach the JavaScript action to the button using a valid action property
            submitBtn.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the form collection
            doc.Form.Add(submitBtn);

            // -------------------------------------------------
            // Save the PDF document
            // -------------------------------------------------
            doc.Save("FormWithValidation.pdf");
        }

        Console.WriteLine("PDF with required-field validation created successfully.");
    }
}
