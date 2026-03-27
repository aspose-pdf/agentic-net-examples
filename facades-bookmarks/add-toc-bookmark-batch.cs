using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder = "input_pdfs";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputFileName = Path.GetFileNameWithoutExtension(fileName) + "_toc.pdf";

            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
                    {
                        editor.BindPdf(doc);

                        // Create a top‑level bookmark titled "Table of Contents" pointing to the first page.
                        Bookmark tocBookmark = new Bookmark();
                        tocBookmark.Title = "Table of Contents";
                        tocBookmark.PageNumber = 1;
                        editor.CreateBookmarks(tocBookmark);

                        // Save the modified PDF with a simple filename (no directory path).
                        editor.Save(outputFileName);
                    }
                }

                Console.WriteLine($"Processed '{fileName}' → '{outputFileName}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}