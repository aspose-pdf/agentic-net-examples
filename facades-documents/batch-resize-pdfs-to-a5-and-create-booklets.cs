using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchResizeAndBooklet
{
    static void Main()
    {
        // Folder containing source PDFs
        const string sourceFolder = @"C:\PdfSource";
        // Folder where final booklets will be saved
        const string outputFolder = @"C:\PdfBooklets";
        // Temporary folder for intermediate resized PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfResizeTemp");

        // Ensure output and temp directories exist
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(tempFolder);

        // Process each PDF file in the source folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string resizedPath = Path.Combine(tempFolder, fileNameWithoutExt + "_A5.pdf");
                string bookletPath = Path.Combine(outputFolder, fileNameWithoutExt + "_booklet.pdf");

                // ---------- Resize to A5 ----------
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Set each page size to A5
                    for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                    {
                        pdfDoc.Pages[i].SetPageSize(PageSize.A5.Width, PageSize.A5.Height);
                    }

                    // Save the resized PDF to a temporary file
                    pdfDoc.Save(resizedPath);
                }

                // ---------- Create Booklet ----------
                // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block.
                PdfFileEditor fileEditor = new PdfFileEditor();
                bool success = fileEditor.MakeBooklet(resizedPath, bookletPath);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to create booklet for '{pdfPath}'.");
                }

                // Clean up the intermediate resized file
                File.Delete(resizedPath);

                Console.WriteLine($"Processed '{pdfPath}' -> booklet saved as '{bookletPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        // Optionally remove the temporary folder if empty
        try
        {
            if (Directory.Exists(tempFolder) && Directory.GetFiles(tempFolder).Length == 0)
                Directory.Delete(tempFolder);
        }
        catch { /* ignore cleanup errors */ }
    }
}
