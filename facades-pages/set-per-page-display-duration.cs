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
        Document doc = new Document(inputPath);

        // Use PdfPageEditor to set the display duration per page
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the already‑loaded Document instance
            editor.BindPdf(doc);

            // Pages are 1‑based; increase duration by one second for each page index
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                editor.ProcessPages = new int[] { i }; // target current page
                editor.DisplayDuration = i;            // seconds
                editor.ApplyChanges();                // persist change for this page
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated DisplayDuration to '{outputPath}'.");
    }
}
