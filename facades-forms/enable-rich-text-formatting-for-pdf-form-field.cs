using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;                // Facade API
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;            // RichTextBoxField

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor facade (required by task)
            FormEditor formEditor = new FormEditor(doc);

            // Optional: set visual appearance for fields via the facade
            formEditor.Facade = new FormFieldFacade();
            // NOTE: FormFieldFacade properties expect System.Drawing.Color, so use fully‑qualified names
            formEditor.Facade.TextColor       = System.Drawing.Color.Black;
            formEditor.Facade.BackgroundColor = System.Drawing.Color.White;
            formEditor.Facade.BorderColor     = System.Drawing.Color.Gray;

            // Retrieve the existing field named "Comments"
            RichTextBoxField commentsField = doc.Form["Comments"] as RichTextBoxField;
            if (commentsField == null)
            {
                Console.Error.WriteLine("Field 'Comments' not found or is not a RichTextBoxField.");
                return;
            }

            // Enable multiline (rich‑text fields are typically multiline)
            commentsField.Multiline = true;

            // Set the default appearance – the constructor expects System.Drawing.Color
            commentsField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Allow rich‑text formatting by assigning a formatted (HTML‑like) value.
            // The markup language used by Aspose.Pdf for rich‑text is a subset of HTML.
            commentsField.FormattedValue = "<b>Enter comments here...</b>";

            // Save the updated PDF (lifecycle rule: Save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rich‑text enabled field: {outputPath}");
    }
}
