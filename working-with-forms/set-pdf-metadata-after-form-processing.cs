using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "form_input.pdf";
        const string outputPath = "form_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document containing form fields
        using (Document doc = new Document(inputPath))
        {
            // ----- Form processing (example) -----
            // Fill a text field named "Name" if it exists
            Form form = doc.Form;
            if (form != null && form["Name"] != null)
            {
                // The indexer returns a generic WidgetAnnotation; cast to the concrete field type
                if (form["Name"] is TextBoxField textBox)
                {
                    textBox.Value = "John Doe"; // or textBox.Text = "John Doe" depending on version
                }
            }

            // ----- Set document metadata -----
            doc.Info.Author    = "Acme Corp";
            doc.Info.Title     = "Completed Form Document";
            doc.Info.Subject   = "Form processing result";
            doc.Info.Keywords  = "form, processed, pdf";

            // Save the updated PDF with the new metadata
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated metadata to '{outputPath}'.");
    }
}
