using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "Comments" and cast it to RichTextBoxField
            RichTextBoxField commentsField = doc.Form["Comments"] as RichTextBoxField;

            if (commentsField == null)
            {
                Console.Error.WriteLine("Field \"Comments\" not found or is not a RichTextBoxField.");
                return;
            }

            // Set the default appearance (font, size, color) – required for rich‑text editing
            commentsField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Enable multiline (rich‑text) editing
            commentsField.Multiline = true;

            // Optionally clear any existing rich‑text content
            commentsField.RichTextValue = string.Empty;

            // Save the changes using the FormEditor facade (as required)
            using (FormEditor editor = new FormEditor(doc))
            {
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Rich‑text field \"Comments\" updated and saved to '{outputPath}'.");
    }
}