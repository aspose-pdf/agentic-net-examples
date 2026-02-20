using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MovePdfFormField
{
    static void Main(string[] args)
    {
        // Input PDF containing the form field
        const string inputPdfPath = "input.pdf";
        // Output PDF with the field moved
        const string outputPdfPath = "output.pdf";
        // Fully qualified name of the field to move
        const string fieldName = "MyFormField";

        // New position for the field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        // Adjust these values as needed
        const float llx = 100f;
        const float lly = 150f;
        const float urx = 300f;
        const float ury = 200f;

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the FormEditor facade
            using (FormEditor formEditor = new FormEditor())
            {
                // Load the PDF document
                formEditor.BindPdf(inputPdfPath);

                // Move the specified field to the new rectangle
                formEditor.MoveField(fieldName, llx, lly, urx, ury);

                // Retrieve the underlying Document object
                Document pdfDocument = formEditor.Document;

                // Save the modified document (uses the provided document-save rule)
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"Field \"{fieldName}\" moved successfully. Output saved to \"{outputPdfPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}