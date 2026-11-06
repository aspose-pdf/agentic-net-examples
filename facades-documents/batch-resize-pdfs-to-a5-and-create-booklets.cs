using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (cross‑platform safe)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Folder containing source PDFs
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        // Folder where resized PDFs will be placed
        string resizedFolder = Path.Combine(baseDir, "ResizedA5");
        // Folder where booklet PDFs will be placed
        string bookletFolder = Path.Combine(baseDir, "Booklets");

        // Ensure folders exist (creates them if missing)
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(resizedFolder);
        Directory.CreateDirectory(bookletFolder);

        // A5 page size in points (1 point = 1/72 inch)
        // Width = 148 mm, Height = 210 mm
        const double a5Width = 148.0 / 25.4 * 72.0;   // ≈ 419.53
        const double a5Height = 210.0 / 25.4 * 72.0;  // ≈ 595.28

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string resizedPath = Path.Combine(resizedFolder, fileName + "_A5.pdf");
                string bookletPath = Path.Combine(bookletFolder, fileName + "_Booklet.pdf");

                // ---------- Resize to A5 ----------
                using (var document = new Document(pdfPath))
                {
                    foreach (Page page in document.Pages)
                    {
                        page.PageInfo.Width = a5Width;
                        page.PageInfo.Height = a5Height;
                    }
                    document.Save(resizedPath);
                }

                // ---------- Create booklet ----------
                var fileEditor = new PdfFileEditor();
                fileEditor.MakeBooklet(resizedPath, bookletPath);

                Console.WriteLine($"Processed '{fileName}': resized -> '{resizedPath}', booklet -> '{bookletPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch operation completed.");
    }
}
