using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input PDF file path.
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Output TeX file path.
        string texPath = Path.Combine(dataDir, "output.tex");

        // Ensure the input file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // Page manipulation using the PdfPageEditor façade.
            // -----------------------------------------------------------------
            // Rotate the first page by 90 degrees while preserving its layout.
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor.
                pageEditor.BindPdf(pdfDocument);

                // Specify which pages to process (1‑based indexing).
                pageEditor.ProcessPages = new int[] { 1 };

                // Set the desired rotation (must be 0, 90, 180 or 270).
                pageEditor.Rotation = 90;

                // Apply the changes to the document.
                pageEditor.ApplyChanges();
            }

            // -----------------------------------------------------------------
            // Export the (now manipulated) PDF to TeX format.
            // -----------------------------------------------------------------
            // Initialize TeX save options – default constructor is sufficient.
            TeXSaveOptions texOptions = new TeXSaveOptions();

            // Save the document as a TeX file using the options.
            pdfDocument.Save(texPath, texOptions);
        }

        Console.WriteLine($"PDF has been processed and saved as TeX: '{texPath}'");
    }
}