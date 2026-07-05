using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the PDF using the Facade API
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // Access the underlying Document
        Document doc = formEditor.Document;

        // Retrieve the "Comments" field and cast to RichTextBoxField
        RichTextBoxField commentsField = doc.Form["Comments"] as RichTextBoxField;
        if (commentsField == null)
        {
            Console.Error.WriteLine("RichTextBoxField \"Comments\" not found.");
            formEditor.Close();
            return;
        }

        // Enable multiline (allows richer content)
        commentsField.Multiline = true;

        // Set a default appearance (font, size, color) for the rich text field
        commentsField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

        // Example rich text value with simple markup (you can replace with actual RTF/HTML as needed)
        commentsField.RichTextValue = "<b>Bold comment</b> <i>italic text</i>";

        // Save the modified PDF using the Facade API
        formEditor.Save(outputPath);
        formEditor.Close();

        Console.WriteLine($"Rich text field \"Comments\" updated and saved to '{outputPath}'.");
    }
}