using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing a RichTextBox field
        const string outputPdf = "output.pdf";         // PDF after setting rich text
        const string fieldName = "RichTextField1";     // name of the RichTextBox field to modify

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the field by name and cast to RichTextBoxField
            if (doc.Form != null && doc.Form[fieldName] is RichTextBoxField richField)
            {
                // HTML markup to be displayed inside the rich text box
                string htmlContent = "<b>Bold Text</b> and <i>Italic Text</i><br/><u>Underlined</u>";

                // Set the formatted rich text value (HTML markup)
                richField.FormattedValue = htmlContent;

                // Optionally, also set the plain rich text value (without markup) if needed
                // richField.RichTextValue = "Bold Text and Italic Text\nUnderlined";

                // Save the modified PDF
                doc.Save(outputPdf);
                Console.WriteLine($"Rich text field '{fieldName}' updated and saved to '{outputPdf}'.");
            }
            else
            {
                Console.Error.WriteLine($"RichTextBox field '{fieldName}' not found in the document.");
            }
        }
    }
}
