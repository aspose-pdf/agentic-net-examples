using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- Form processing (example) -----
                // Check if the document contains a form and set a field value
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    // The indexer returns a Field (derived from WidgetAnnotation).
                    // Cast to Field to access the Value property safely.
                    Field nameField = doc.Form["Name"] as Field;
                    if (nameField != null)
                    {
                        nameField.Value = "John Doe";
                    }
                }

                // ----- Set PDF metadata -----
                // Standard metadata properties are available via the Info dictionary
                doc.Info.Author   = "Jane Smith";
                doc.Info.Title    = "Processed Form Document";
                doc.Info.Subject  = "Form processing example";
                doc.Info.Keywords = "Aspose.Pdf, form, metadata";

                // Save the modified PDF (PDF format, no SaveOptions needed)
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
