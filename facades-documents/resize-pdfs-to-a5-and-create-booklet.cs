using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder to store intermediate A5‑sized PDFs
        const string resizedFolder = "ResizedA5";
        // Folder to store final booklet PDFs
        const string outputFolder = "Booklets";

        // Ensure the required folders exist. If the input folder is missing we abort with a clear message.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please create it and place PDF files inside.");
            return;
        }
        Directory.CreateDirectory(resizedFolder);
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string resizedPath = Path.Combine(resizedFolder, fileName + "_A5.pdf");
            string bookletPath = Path.Combine(outputFolder, fileName + "_Booklet.pdf");

            // ---------- Resize to A5 ----------
            // PdfPageEditor changes the page size of the document.
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(inputPath);          // Load source PDF
                pageEditor.PageSize = PageSize.A5;      // Set target page size (A5)
                pageEditor.ApplyChanges();              // Apply the size change
                pageEditor.Save(resizedPath);           // Save the resized PDF
            }

            // ---------- Create booklet ----------
            // PdfFileEditor makes a booklet from the resized PDF.
            PdfFileEditor fileEditor = new PdfFileEditor();
            // The overload with PageSize ensures the booklet pages are also A5.
            fileEditor.MakeBooklet(resizedPath, bookletPath, PageSize.A5);
        }

        Console.WriteLine("Processing completed.");
    }
}
