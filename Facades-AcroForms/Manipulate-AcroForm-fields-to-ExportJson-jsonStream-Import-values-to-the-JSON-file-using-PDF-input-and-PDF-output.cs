using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ---------- Export form fields to JSON ----------
            // Create Form facade and bind the source PDF
            var exportForm = new Form();
            exportForm.BindPdf(inputPdfPath);

            // Export all field values (except passwords) to a memory stream
            using (var jsonStream = new MemoryStream())
            {
                // The second parameter indicates whether password values should be exported
                exportForm.ExportJson(jsonStream, false);
                jsonStream.Position = 0; // Reset stream for reading

                // ---------- Import JSON values into a PDF ----------
                // For demonstration we import into a copy of the original PDF.
                // In real scenarios this could be a different template PDF.
                var importForm = new Form();
                importForm.BindPdf(inputPdfPath); // bind the same PDF or another one

                // Import the previously exported JSON data
                importForm.ImportJson(jsonStream);

                // Save the modified PDF using the Document object (document-save rule)
                Document resultDoc = importForm.Document;
                resultDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields exported to JSON and re‑imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}