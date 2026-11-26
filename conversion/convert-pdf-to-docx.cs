using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.docx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // For non‑PDF output you must supply a SaveOptions subclass.
                // DocSaveOptions with Format = DocX tells Aspose.Pdf to write a DOCX file.
                DocSaveOptions saveOpts = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save the document as DOCX
                doc.Save(outputPath, saveOpts);
            }

            Console.WriteLine($"PDF successfully saved as DOCX to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}