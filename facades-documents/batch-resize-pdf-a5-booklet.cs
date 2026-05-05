using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchBookletCreator
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where resized PDFs and booklets will be saved
        const string outputFolder = "OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Ensure input directory exists – if it does not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now. Place PDF files in this folder and re‑run the program.");
            Directory.CreateDirectory(inputFolder);
            return; // Nothing to process yet
        }

        // Process each PDF file in the input folder
        foreach (string sourceFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourceFile);

            // Path for the intermediate A5‑sized PDF
            string resizedFile = Path.Combine(outputFolder, $"{fileNameWithoutExt}_A5.pdf");
            // Path for the final booklet PDF
            string bookletFile = Path.Combine(outputFolder, $"{fileNameWithoutExt}_A5_booklet.pdf");

            // ---------- Resize to A5 ----------
            using (Document doc = new Document(sourceFile))
            {
                // PdfPageEditor changes page size; it does not implement IDisposable
                PdfPageEditor pageEditor = new PdfPageEditor();
                pageEditor.BindPdf(doc);
                pageEditor.PageSize = PageSize.A5;   // Set target page size to A5
                pageEditor.ApplyChanges();           // Apply the size change

                // Save the resized document
                doc.Save(resizedFile);
            }

            // ---------- Create booklet ----------
            PdfFileEditor fileEditor = new PdfFileEditor();
            // Use the overload that accepts a PageSize to ensure the booklet pages are A5 as well
            fileEditor.MakeBooklet(resizedFile, bookletFile, PageSize.A5);

            // Optional: delete the intermediate resized file if it is no longer needed
            // File.Delete(resizedFile);

            Console.WriteLine($"Processed '{Path.GetFileName(sourceFile)}' -> booklet saved as '{Path.GetFileName(bookletFile)}'");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
