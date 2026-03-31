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

        // Verify that the source PDF exists before processing
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found.");
            return;
        }

        // Load the PDF document first (FormEditor expects a Document, not a file path)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Use the non‑obsolete FormEditor constructor that takes a Document instance
            using (FormEditor formEditor = new FormEditor(pdfDoc))
            {
                // Set the maximum character length for the "PhoneNumber" field
                bool success = formEditor.SetFieldLimit("PhoneNumber", 15);
                Console.WriteLine(success ? "Field limit set successfully." : "Failed to set field limit.");

                // Persist the changes to a new PDF file
                formEditor.Save(outputPath);
            }
        }
    }
}