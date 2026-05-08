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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor and bind the document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set display duration per page (1 second for first page, incrementing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                editor.ProcessPages = new int[] { i };
                editor.DisplayDuration = i; // seconds
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with updated display durations to '{outputPath}'.");
    }
}