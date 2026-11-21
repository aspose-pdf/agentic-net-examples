using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string newAuthor = "John Doe";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Bind to the PDF file and modify its metadata without loading the full document.
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(pdfPath);          // Attach the PDF.
                pdfInfo.Author = newAuthor;        // Set the new Author value.
                // SaveNewInfo requires an output path; passing the same path overwrites the original file.
                pdfInfo.SaveNewInfo(pdfPath);
            }

            Console.WriteLine($"Author updated to \"{newAuthor}\" in '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
