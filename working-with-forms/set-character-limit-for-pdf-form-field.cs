using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // FormEditor resides in Facades namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "CustomerName";
        const int    maxLength  = 50;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // FormEditor provides high‑level form manipulation utilities
            FormEditor formEditor = new FormEditor(doc);

            // Set the maximum number of characters allowed in the specified text field
            formEditor.SetFieldLimit(fieldName, maxLength);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' limited to {maxLength} characters and saved to '{outputPath}'.");
    }
}