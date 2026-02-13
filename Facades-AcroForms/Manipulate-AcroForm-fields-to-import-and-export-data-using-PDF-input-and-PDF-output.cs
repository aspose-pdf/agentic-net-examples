using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF, the FDF file to import,
        // the resulting PDF and a simple export of field values.
        string inputPdfPath = "input.pdf";
        string fdfImportPath = "data.fdf";
        string outputPdfPath = "output.pdf";
        string exportTxtPath = "exported.txt";

        // Verify that the required files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfImportPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {fdfImportPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdfPath);

        // --------------------------------------------------------------------
        // Import form data from the FDF file.
        // FdfReader reads annotations and field values and merges them into
        // the document's AcroForm.
        // --------------------------------------------------------------------
        using (FileStream fdfStream = File.OpenRead(fdfImportPath))
        {
            FdfReader.ReadAnnotations(fdfStream, pdfDocument);
        }

        // --------------------------------------------------------------------
        // Export current form field values to a simple text file.
        // Each line contains "fieldName=fieldValue".
        // --------------------------------------------------------------------
        using (StreamWriter writer = new StreamWriter(exportTxtPath, false))
        {
            foreach (Field field in pdfDocument.Form)
            {
                // FullName is the qualified name; fall back to Name if null.
                string fieldName = field.FullName ?? field.Name ?? "(unnamed)";
                string fieldValue = field.Value?.ToString() ?? string.Empty;
                writer.WriteLine($"{fieldName}={fieldValue}");
            }
        }

        // Save the modified PDF document.
        pdfDocument.Save(outputPdfPath);
    }
}