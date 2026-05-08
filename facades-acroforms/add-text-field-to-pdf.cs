using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "MyTextField";
        const int pageNumber = 1; // 1‑based page index
        const float llx = 100f;   // lower‑left X
        const float lly = 200f;   // lower‑left Y
        const float urx = 300f;   // upper‑right X
        const float ury = 250f;   // upper‑right Y

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Initialize FormEditor with the loaded document
                FormEditor formEditor = new FormEditor(doc);

                // Add a new text field at the specified coordinates
                bool added = formEditor.AddField(FieldType.Text, fieldName, pageNumber, llx, lly, urx, ury);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the text field.");
                }

                // Save the modified PDF to the output path
                formEditor.Save(outputPdf);
            }

            Console.WriteLine($"Text field added and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}