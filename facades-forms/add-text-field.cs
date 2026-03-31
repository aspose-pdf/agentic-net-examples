using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Add a text field named "CustomerName" on page 1
        // Rectangle coordinates: lower‑left (100, 500), upper‑right (300, 530)
        bool fieldAdded = formEditor.AddField(FieldType.Text, "CustomerName", 1, 100f, 500f, 300f, 530f);

        if (fieldAdded)
        {
            Console.WriteLine("Text field 'CustomerName' added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add text field.");
        }

        // Persist changes to the output PDF
        formEditor.Save();
    }
}