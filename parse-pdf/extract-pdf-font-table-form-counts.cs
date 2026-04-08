using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for Font collections

class Program
{
    // Entry point: expects one or more PDF file paths as command‑line arguments.
    static void Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("Please provide at least one PDF file path as an argument.");
            return;
        }

        foreach (string pdfPath in args)
        {
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Number of fonts used in the document.
                // Aspose.Pdf does not expose a direct Document.Fonts collection; fonts are stored per‑page in Resources.Fonts.
                int fontCount = 0;
                foreach (Page page in doc.Pages)
                {
                    // Resources may be null for some pages; guard against it.
                    fontCount += page.Resources?.Fonts?.Count ?? 0;
                }

                // Number of tables: iterate all pages and count Table objects in the page paragraphs.
                int tableCount = 0;
                foreach (Page page in doc.Pages)
                {
                    tableCount += page.Paragraphs.OfType<Table>().Count();
                }

                // Number of form fields: the Form object may be null if there are no fields.
                // Use Count() extension method with null‑conditional operator to avoid the "method group cannot be made nullable" error.
                int formFieldCount = doc.Form?.Fields?.Count() ?? 0;

                // Log the statistics.
                Console.WriteLine($"Document: {Path.GetFileName(pdfPath)}");
                Console.WriteLine($"  Fonts: {fontCount}");
                Console.WriteLine($"  Tables: {tableCount}");
                Console.WriteLine($"  Form fields: {formFieldCount}");
            }
        }
    }
}
