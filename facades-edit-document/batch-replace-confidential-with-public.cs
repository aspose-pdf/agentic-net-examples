using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchReplace
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputFolder = @"C:\PdfArchive";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (lifecycle: using block ensures disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a PdfContentEditor facade and bind the document
                    PdfContentEditor editor = new PdfContentEditor();
                    editor.BindPdf(doc);

                    // Replace all occurrences of "Confidential" with "Public" (all pages)
                    editor.ReplaceText("Confidential", "Public");

                    // Save the modified document, overwriting the original file
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}