using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_data.json";

        // Ensure the input PDF exists; create a simple one with a form field if it does not.
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdfWithForm(inputPdf);
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Prepare JSON export options with pretty‑printing (indented)
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                WriteIndented = true // enable indentation for readability
            };

            // Export all form fields to a JSON file
            using (FileStream fs = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(fs, jsonOptions);
            }
        }

        Console.WriteLine($"Form data exported to '{outputJson}'.");
    }

    private static void CreateSamplePdfWithForm(string path)
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define a rectangle for the text box field (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Add a text box field to the form
            TextBoxField txt = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleText",
                Value = "Hello Aspose"
            };
            doc.Form.Add(txt, 1);

            // Save the PDF so it can be used for the export example
            doc.Save(path);
        }
    }
}