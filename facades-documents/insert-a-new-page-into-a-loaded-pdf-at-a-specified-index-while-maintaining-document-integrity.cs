using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the output PDF, and the temporary blank page PDF
        const string sourcePdf = "source.pdf";
        const string outputPdf = "output.pdf";
        // Insert position is 1‑based. For example, 2 inserts after the first page.
        const int insertIndex = 2;

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Create a temporary blank PDF containing a single empty page
        string tempBlankPath = Path.ChangeExtension(Path.GetTempFileName(), ".pdf");
        try
        {
            using (Document blankDoc = new Document())
            {
                // Insert an empty page at position 1 (pages are 1‑based)
                blankDoc.Pages.Insert(1);
                blankDoc.Save(tempBlankPath);
            }

            // Use PdfFileEditor (Facades API) to insert the blank page into the source PDF
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the page from the temporary blank PDF.
            // Parameters: inputFile, insertLocation, portFile, startPage, endPage, outputFile
            bool result = editor.Insert(sourcePdf, insertIndex, tempBlankPath, 1, 1, outputPdf);

            Console.WriteLine(result
                ? $"Page inserted successfully. Output saved to '{outputPdf}'."
                : "Page insertion failed.");
        }
        finally
        {
            // Remove the temporary blank PDF
            if (File.Exists(tempBlankPath))
                File.Delete(tempBlankPath);
        }
    }
}