using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and field parameters
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_textfield.pdf";

        // Coordinates for the new text field (lower‑left and upper‑right corners)
        const int    pageNumber = 1;      // 1‑based page index
        const float  llx        = 100f;   // lower‑left X
        const float  lly        = 200f;   // lower‑left Y
        const float  urx        = 300f;   // upper‑right X
        const float  ury        = 220f;   // upper‑right Y

        // Field name (must be unique within the document)
        const string fieldName = "MyTextField";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // FormEditor constructor takes source and destination file names
            using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
            {
                // Add a text field at the specified location
                bool added = formEditor.AddField(FieldType.Text, fieldName,
                                                pageNumber, llx, lly, urx, ury);

                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the text field.");
                }

                // Persist changes to the output PDF
                formEditor.Save();
            }

            Console.WriteLine($"Text field added and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}