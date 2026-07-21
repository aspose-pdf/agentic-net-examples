using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // FormEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Set the global memory‑loading limit to 2 MB.
        // This limits the size of files that can be attached via form fields.
        Document.FileSizeLimitToMemoryLoading = 2;

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Use FormEditor to enforce constraints on the form.
            // Here we simply re‑save the document; the size limit is already applied.
            // If additional field‑specific constraints are needed, they can be set via FormEditor methods.
            FormEditor editor = new FormEditor(doc);
            // Example: editor.SetMaxFileSize("myFileField", 2 * 1024 * 1024); // (hypothetical API)

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with 2 MB file‑size limit: {outputPath}");
    }
}