using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "form_input.pdf";
        const string outputPath = "form_filled_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and ensure deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ---- Form processing example ----
                // Locate a text box field named "NameField" and set its value
                if (doc.Form["NameField"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }

                // ---- Set PDF metadata ----
                doc.Info.Author = "Acme Corp";
                doc.Info.Title  = "Completed Form";

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with metadata to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}