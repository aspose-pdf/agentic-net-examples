using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains form fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Retrieve the text box field that will hold rich text (replace with your field name)
            TextBoxField richField = doc.Form["RichTextField"] as TextBoxField;
            if (richField == null)
            {
                Console.WriteLine("RichTextField not found or not a TextBoxField.");
                return;
            }

            // Define HTML markup for rich text (supports a subset of HTML tags)
            string html = "<b>Bold Text</b> <i>Italic Text</i> <u>Underlined</u> " +
                          "<font color='red'>Red Text</font> " +
                          "<br/>New line with <span style='font-size:14pt'>larger font</span>";

            // Aspose.Pdf versions prior to the introduction of the RichText property do not expose a direct API
            // for setting the /RV (rich value) entry. As a workaround, assign the HTML markup to the field's
            // Value property. When the PDF viewer supports rich text fields, it will render the markup.
            // If the viewer does not support it, the markup will appear as plain text.
            richField.Value = html;

            // Optionally set a plain‑text fallback for viewers that do not support rich text.
            // In this example we reuse the same string without HTML tags.
            // (If you need a true plain‑text fallback, provide a stripped‑down version here.)
            // richField.Value = "Bold Text Italic Text Underlined Red Text"; // uncomment if needed

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rich text field to '{outputPath}'.");
    }
}
