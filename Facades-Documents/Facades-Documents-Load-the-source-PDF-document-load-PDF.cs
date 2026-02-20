using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document using the Form facade from Aspose.Pdf.Facades
        using (Form pdfForm = new Form())
        {
            pdfForm.BindPdf(inputPath); // Load the PDF

            // Access the underlying Document object if needed
            var document = pdfForm.Document;

            // Example output: number of pages in the loaded PDF
            Console.WriteLine($"PDF loaded successfully. Page count: {document.Pages.Count}");
        }
    }
}