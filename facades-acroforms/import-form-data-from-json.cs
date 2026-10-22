using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form_template.pdf";
        const string outputPdfPath = "form_filled.pdf";
        const string jsonPath = "data.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load JSON into a stream – ImportJson expects a Stream, not a file path.
            string jsonText = File.ReadAllText(jsonPath);
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));

            // Bind the PDF form and import the JSON data.
            // Aspose.Pdf.Facades.Form is used (fully‑qualified) to avoid ambiguity with Aspose.Pdf.Forms.Form.
            using var pdfForm = new Aspose.Pdf.Facades.Form();
            pdfForm.BindPdf(inputPdfPath);
            pdfForm.ImportJson(jsonStream); // Missing fields are ignored automatically.
            pdfForm.Save(outputPdfPath);

            Console.WriteLine($"Form data imported successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}