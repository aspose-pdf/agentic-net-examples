using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Initialize FormEditor with the loaded document
                FormEditor formEditor = new FormEditor(doc);

                // List of field names to be removed – adjust as needed
                string[] fieldsToDelete = { "TextBox1", "ListBox1", "CheckBox1" };

                // Remove each field; RemoveField silently ignores non‑existent names
                foreach (string fieldName in fieldsToDelete)
                {
                    formEditor.RemoveField(fieldName);
                }

                // Save the modified PDF
                formEditor.Save(outputPdf);
            }

            Console.WriteLine($"Specified fields have been removed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}