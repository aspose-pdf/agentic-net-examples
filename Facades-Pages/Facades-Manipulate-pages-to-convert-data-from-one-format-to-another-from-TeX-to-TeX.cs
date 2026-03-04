using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class TeXPageManipulation
{
    static void Main()
    {
        // Paths for input TeX file and final output TeX file
        const string inputTexPath  = "input.tex";
        const string outputTexPath = "output.tex";

        // Verify input file exists
        if (!File.Exists(inputTexPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputTexPath}");
            return;
        }

        // Load the TeX file into a PDF Document using TeXLoadOptions
        TeXLoadOptions loadOptions = new TeXLoadOptions();
        using (Document pdfDoc = new Document(inputTexPath, loadOptions))
        {
            // Create a PdfPageEditor facade to manipulate page properties
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the PDF document to the editor
                pageEditor.BindPdf(pdfDoc);

                // Example manipulation: rotate the first page 90 degrees clockwise
                // ProcessPages specifies which pages the editor will affect (1‑based indexing)
                pageEditor.ProcessPages = new int[] { 1 };
                pageEditor.Rotation = 90; // Valid values: 0, 90, 180, 270

                // Apply the changes to the document
                pageEditor.ApplyChanges();
            }

            // Save the modified document back to TeX format using TeXSaveOptions
            TeXSaveOptions saveOptions = new TeXSaveOptions();
            pdfDoc.Save(outputTexPath, saveOptions);
        }

        Console.WriteLine($"TeX file processed and saved to '{outputTexPath}'.");
    }
}