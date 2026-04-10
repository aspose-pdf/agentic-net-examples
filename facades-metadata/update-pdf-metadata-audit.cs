using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and CSV file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string csvLogPath    = "metadata_audit.csv";

        // New metadata values to be applied
        const string newTitle   = "New Document Title";
        const string newAuthor  = "Jane Doe";
        const string newSubject = "Updated Subject";
        const string newKeywords = "Aspose,PDF,Metadata";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the document to PdfFileInfo facade to read/write metadata
            using (PdfFileInfo info = new PdfFileInfo(pdfDoc))
            {
                // Capture original metadata values
                string originalTitle    = info.Title   ?? string.Empty;
                string originalAuthor   = info.Author  ?? string.Empty;
                string originalSubject  = info.Subject ?? string.Empty;
                string originalKeywords = info.Keywords ?? string.Empty;

                // Apply new metadata values
                info.Title    = newTitle;
                info.Author   = newAuthor;
                info.Subject  = newSubject;
                info.Keywords = newKeywords;

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
                
                // Write audit information to CSV
                using (StreamWriter writer = new StreamWriter(csvLogPath, false))
                {
                    // CSV header
                    writer.WriteLine("Property,OriginalValue,NewValue");

                    // Helper to escape commas and quotes
                    string Escape(string s) => $"\"{s.Replace("\"", "\"\"")}\"";

                    writer.WriteLine($"Title,{Escape(originalTitle)},{Escape(newTitle)}");
                    writer.WriteLine($"Author,{Escape(originalAuthor)},{Escape(newAuthor)}");
                    writer.WriteLine($"Subject,{Escape(originalSubject)},{Escape(newSubject)}");
                    writer.WriteLine($"Keywords,{Escape(originalKeywords)},{Escape(newKeywords)}");
                }

                Console.WriteLine($"Metadata updated and audit log written to '{csvLogPath}'.");
            }
        }
    }
}