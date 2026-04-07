using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade to manipulate form fields
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // Add a push‑button field named "ExportBtn" on page 1
                // Parameters: FieldType, field name, page number, llx, lly, urx, ury
                formEditor.AddField(FieldType.PushButton, "ExportBtn", 1, 100, 500, 200, 550);

                // JavaScript that exports all annotations to a JSON string and shows it in an alert
                string js = "app.alert(JSON.stringify(this.getAnnots()));";

                // Attach the JavaScript to the button field
                formEditor.AddFieldScript("ExportBtn", js);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with export button saved to '{outputPath}'.");
    }
}