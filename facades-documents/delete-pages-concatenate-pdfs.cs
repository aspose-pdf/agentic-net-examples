using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF to edit
        const string otherPath = "other.pdf";          // PDF to concatenate
        const string tempPath = "temp_edited.pdf";    // intermediate edited file
        const string outputPath = "final.pdf";        // final result

        if (!File.Exists(inputPath) || !File.Exists(otherPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load the first PDF, delete pages, and save to a temporary file
        using (Document doc = new Document(inputPath))
        {
            // Example: delete pages 2 and 3 if they exist.
            // Delete higher page numbers first to keep indices valid (1‑based indexing).
            int pageCount = doc.Pages.Count;
            if (pageCount >= 3)
            {
                doc.Pages.Delete(3);
                doc.Pages.Delete(2);
            }
            else if (pageCount == 2)
            {
                doc.Pages.Delete(2);
            }

            // Save the edited PDF for later concatenation
            doc.Save(tempPath);
        }

        // Concatenate the edited PDF with the second document using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        string[] sources = new string[] { tempPath, otherPath };
        editor.Concatenate(sources, outputPath);

        // Clean up the temporary file
        try { File.Delete(tempPath); } catch { }

        Console.WriteLine($"Concatenated PDF saved to '{outputPath}'.");
    }
}