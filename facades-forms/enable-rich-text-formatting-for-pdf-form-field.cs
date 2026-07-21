using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;          // for DefaultAppearance

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "Comments" and cast it to RichTextBoxField
            RichTextBoxField commentsField = doc.Form["Comments"] as RichTextBoxField;
            if (commentsField == null)
            {
                Console.Error.WriteLine("Field 'Comments' not found or is not a RichTextBoxField.");
                return;
            }

            // Enable multiline (allows richer content) and set a default appearance
            commentsField.Multiline = true;

            // DefaultAppearance is read‑only; use the constructor that accepts font name, size and color
            commentsField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Optionally set an initial rich‑text value (RTF markup) – this demonstrates rich‑text capability
            commentsField.RichTextValue = @"{\rtf1\ansi This is \b bold\b0  and \i italic\i0  text.}";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich text field 'Comments' updated and saved to '{outputPath}'.");
    }
}