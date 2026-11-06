using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // Bookmark class lives in this namespace

class Program
{
    static void Main()
    {
        const string inputFolder = "InputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine the last page number of the PDF
                int lastPage;
                using (Document doc = new Document(pdfPath))
                {
                    lastPage = doc.Pages.Count; // 1‑based page index
                }

                // Create and bind the bookmark editor
                using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
                {
                    editor.BindPdf(pdfPath);

                    // Build a single Bookmark instance for the "Reviewed" entry
                    Bookmark reviewed = new Bookmark
                    {
                        Title = "Reviewed",
                        PageNumber = lastPage,
                        // The bookmark is expanded (open) by default when Open = true
                        Open = true,
                        // Optional visual tweaks – can be omitted
                        BoldFlag = false,
                        ItalicFlag = false,
                        Action = "GoTo" // standard GoTo action for page navigation
                    };

                    // Add the bookmark to the document
                    editor.CreateBookmarks(reviewed);

                    // Persist changes, overwriting the original file
                    editor.Save(pdfPath);
                }

                Console.WriteLine($"Added 'Reviewed' bookmark to: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {Path.GetFileName(pdfPath)}: {ex.Message}");
            }
        }
    }
}
