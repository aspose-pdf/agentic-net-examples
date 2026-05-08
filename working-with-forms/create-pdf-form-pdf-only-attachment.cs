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

            // -----------------------------------------------------------------
            // 1. Create a text box that will hold the file name/path.
            //    We use a TextBoxField because FileSelectBoxField has no public ctor.
            // -----------------------------------------------------------------
            var fileFieldRect = new Rectangle(100, 600, 200, 30);
            TextBoxField fileField = new TextBoxField(page, fileFieldRect)
            {
                PartialName   = "pdfAttachment",
                AlternateName = "Attach PDF file",
                MaxLen        = 260 // typical max path length
            };

            // -----------------------------------------------------------------
            // 2. Add JavaScript validation that runs when the user leaves the field.
            //    The script checks that the entered value ends with ".pdf" (case‑insensitive).
            //    If not, an alert is shown and the field value is rejected.
            // -----------------------------------------------------------------
            string jsValidatePdf = @"if (event.value != null && event.value.length > 0) {
    var lower = event.value.toLowerCase();
    if (!lower.endsWith('.pdf')) {
        app.alert('Only PDF files are allowed.');
        event.rc = false; // cancel the change
    }
}";
            fileField.Actions.OnExit = new JavascriptAction(jsValidatePdf);

            // -----------------------------------------------------------------
            // 3. Add the field to the form (page numbers are 1‑based).
            // -----------------------------------------------------------------
            doc.Form.Add(fileField, 1);

            // -----------------------------------------------------------------
            // OPTIONAL: Add a button that, when clicked, attaches the selected file
            //           to the PDF as an embedded file. The button uses JavaScript
            //           to import the file (the user will be prompted to choose a
            //           file – the same validation applies because the field already
            //           restricts the name to *.pdf).
            // -----------------------------------------------------------------
            var attachBtnRect = new Rectangle(210, 600, 260, 630);
            // Use ButtonField (the current class name in Aspose.Pdf) instead of the removed PushButtonField.
            ButtonField attachBtn = new ButtonField(page, attachBtnRect)
            {
                PartialName   = "attachBtn",
                AlternateName = "Attach"
            };
            // JavaScript that imports the file specified in the text box as an attachment.
            string jsAttach = @"var f = this.getField('pdfAttachment').value;
if (f == null || f == '') { app.alert('Please specify a PDF file first.'); }
else { this.importData({cName:f}); }";
            // Use a valid action property for button clicks.
            attachBtn.Actions.OnPressMouseBtn = new JavascriptAction(jsAttach);
            doc.Form.Add(attachBtn, 1);

            // Save the PDF with the form fields
            doc.Save("fileAttachmentForm.pdf");
        }

        Console.WriteLine("PDF form with PDF‑only file attachment field created successfully.");
    }
}
