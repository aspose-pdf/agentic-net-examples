using System;
using System.IO;
using System.Drawing; // Required for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Comments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the existing RichTextBoxField named "Comments"
            // The Form collection is accessed via the Document's Form property
            // Cast to RichTextBoxField to access rich‑text specific members
            RichTextBoxField commentsField = doc.Form[fieldName] as RichTextBoxField;
            if (commentsField == null)
            {
                Console.Error.WriteLine($"RichTextBoxField \"{fieldName}\" not found.");
                return;
            }

            // Set the default appearance (font, size, color) – the constructor requires System.Drawing.Color
            commentsField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Ensure the field allows multiline (required for rich‑text editing)
            commentsField.Multiline = true;

            // Optionally, enable spell‑check and scrolling for a better editing experience
            commentsField.SpellCheck = true;
            commentsField.Scrollable = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich text field \"{fieldName}\" updated and saved to '{outputPath}'.");
    }
}