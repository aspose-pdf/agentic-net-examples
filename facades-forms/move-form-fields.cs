using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Load the document
        using (Document doc = new Document(inputPath))
        {
            // FormEditor now expects a Document instance (not a file path)
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Ensure the document actually contains a form and fields
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (var field in doc.Form.Fields)
                    {
                        string fieldName = field.Name;
                        var rect = field.Rect;

                        // Apply offset (10, 15)
                        float llx = (float)rect.LLX + 10f;
                        float lly = (float)rect.LLY + 15f;
                        float urx = (float)rect.URX + 10f;
                        float ury = (float)rect.URY + 15f;

                        // Move the field to the new rectangle
                        formEditor.MoveField(fieldName, llx, lly, urx, ury);
                    }
                }

                // Save the modified document to the desired output path
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"All form fields moved and saved to '{outputPath}'.");
    }
}
