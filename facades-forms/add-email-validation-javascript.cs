using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with a text field named "Email"
        using (Document document = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = document.Pages.Add();

            // Define rectangle for the text field
            Rectangle fieldRect = new Rectangle(100, 600, 300, 620);

            // Create the text box field
            TextBoxField emailField = new TextBoxField(page, fieldRect);
            emailField.PartialName = "Email";

            // Add the field to the document form (page index is 1‑based)
            document.Form.Add(emailField, 1);

            // JavaScript to validate email on blur (loss of focus)
            string js = "var email = this.getField('Email').value;" +
                        "var re = /^[\\w\\-\\.]+@([\\w\\-]+\\.)+[\\w\\-]{2,4}$/;" +
                        "if (!re.test(email)) {" +
                        "app.alert('Invalid email address');" +
                        "this.getField('Email').value='';" +
                        "}";

            // Attach the script to the OnLostFocus event (blur)
            emailField.Actions.OnLostFocus = new JavascriptAction(js);

            // Save the PDF
            document.Save("output.pdf");
        }
    }
}
