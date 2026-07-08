using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF that contains a form field named "Email".
        //    This makes the example self‑contained and removes the runtime
        //    FileNotFoundException that occurred when the code tried to open a
        //    non‑existent "input.pdf" file.
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page.
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box (left, bottom, right, top).
            // Use Aspose.Pdf.Rectangle to match the constructor expected by
            // TextBoxField.
            var fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create the text box field and give it the partial name "Email".
            TextBoxField emailField = new TextBoxField(page, fieldRect)
            {
                PartialName = "Email",
                Value = ""
            };
            doc.Form.Add(emailField);

            // Save the PDF into a memory stream – we will bind this stream to the
            // FormEditor facade instead of a physical file.
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream);
                pdfStream.Position = 0; // Reset position for reading.

                // -----------------------------------------------------------------
                // 2. Attach JavaScript validation to the "Email" field using the
                //    FormEditor facade.  The SetFieldScript method works with the
                //    field name and the script text.  The script runs on the blur
                //    (OnBlur) event and shows an alert if the entered value does not
                //    match a simple e‑mail regular expression.
                // -----------------------------------------------------------------
                using (FormEditor formEditor = new FormEditor())
                {
                    // Bind the PDF from the memory stream (see "use‑stream‑overload‑for‑bindpdf").
                    formEditor.BindPdf(pdfStream);

                    string emailValidationJs = @"if (event.target.value.match(/^\\S+@\\S+\\.\\S+$/) == null) {\n    app.alert('Invalid email address');\n}";

                    // Attach the script to the field named "Email".
                    formEditor.SetFieldScript("Email", emailValidationJs);

                    // Save the modified PDF to disk.
                    formEditor.Save(outputPdf);
                }
            }
        }

        Console.WriteLine($"PDF saved with email validation script: {outputPdf}");
    }
}
