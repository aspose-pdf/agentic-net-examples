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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use FormEditor (from Aspose.Pdf.Facades) to set a character limit on the 'CustomerName' field
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);
                formEditor.SetFieldLimit("CustomerName", 50);
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with field limit applied: {outputPath}");
    }
}