using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "readonly_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form and the target field exists
            if (doc.Form != null && doc.Form["CustomerName"] != null)
            {
                // Retrieve the field (TextBoxField inherits WidgetAnnotation)
                TextBoxField field = (TextBoxField)doc.Form["CustomerName"];
                // Mark the field as read‑only to prevent further editing
                field.ReadOnly = true;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only field: {outputPath}");
    }
}