using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where modified PDFs will be saved
        const string outputFolder = "output_pdfs";

        // Author to replace (set to empty string to affect all authors)
        const string sourceAuthor = "";
        // New author name
        const string destinationAuthor = "New Author";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please create it and place PDF files inside before running the program.");
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to process.");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Use PdfAnnotationEditor facade to modify annotation authors
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF
                editor.BindPdf(pdfPath);

                // Determine page range (Aspose.Pdf uses 1‑based indexing)
                int startPage = 1;
                int endPage = editor.Document.Pages.Count;

                // Update the author for all annotations in the specified range
                editor.ModifyAnnotationsAuthor(startPage, endPage, sourceAuthor, destinationAuthor);

                // Save the modified PDF
                editor.Save(outputPath);
                // Close the facade (optional, handled by using)
                editor.Close();
            }
        }

        Console.WriteLine("Annotation authors updated for all PDFs.");
    }
}
