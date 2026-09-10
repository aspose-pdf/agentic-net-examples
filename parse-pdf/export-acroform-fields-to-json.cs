using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // Provides Form and ExportToJson methods

class Program
{
    static void Main()
    {
        // Input PDF containing AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will contain field names and values
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export all AcroForm fields to JSON.
            // The ExportToJson method writes the JSON representation directly to the specified file.
            pdfDoc.Form.ExportToJson(outputJsonPath);
        }

        Console.WriteLine($"AcroForm fields exported to '{outputJsonPath}'.");
    }
}