using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "securityAnswer";
        const int    limit      = 30;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a FormEditor and bind it to the loaded document
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(pdfDoc);

            // Set the maximum number of characters allowed for the field
            formEditor.SetFieldLimit(fieldName, limit);

            // Save the modified document to the specified output file
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' limited to {limit} characters. Saved to '{outputPath}'.");
    }
}