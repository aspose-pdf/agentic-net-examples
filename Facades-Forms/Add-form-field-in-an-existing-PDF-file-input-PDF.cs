using System;
using System.IO;
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

        // Initialize FormEditor with source and destination PDF files
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a text field named "NewTextField" on page 1.
            // Rectangle coordinates: lower‑left (100, 500), upper‑right (250, 530)
            bool success = formEditor.AddField(FieldType.Text, "NewTextField", 1, 100, 500, 250, 530);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add the form field.");
            }

            // Persist changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Form field added and saved to '{outputPdf}'.");
    }
}