using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string csvPath = "metadata_audit.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind to the source PDF using the Facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Capture original metadata values
            string origTitle = pdfInfo.Title;
            string origAuthor = pdfInfo.Author;
            string origSubject = pdfInfo.Subject;
            string origKeywords = pdfInfo.Keywords;
            string origCreator = pdfInfo.Creator;
            string origCreationDate = pdfInfo.CreationDate; // string in PDF date format
            string origModDate = pdfInfo.ModDate;           // string in PDF date format

            // Define new metadata values (example values)
            string newTitle = "New Document Title";
            string newAuthor = "John Doe";
            string newSubject = "Updated Subject";
            string newKeywords = "Aspose,PDF,Metadata";
            string newCreator = "Aspose.Pdf.Facades";
            // PdfFileInfo expects dates as PDF‑date formatted strings (yyyyMMddHHmmss)
            string newCreationDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string newModDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Apply new metadata
            pdfInfo.Title = newTitle;
            pdfInfo.Author = newAuthor;
            pdfInfo.Subject = newSubject;
            pdfInfo.Keywords = newKeywords;
            pdfInfo.Creator = newCreator;
            pdfInfo.CreationDate = newCreationDate;
            pdfInfo.ModDate = newModDate;

            // Save the updated PDF (creates a new file)
            bool saved = pdfInfo.SaveNewInfo(outputPdf);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save the updated PDF.");
                return;
            }

            // Write audit information to CSV
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                writer.WriteLine("Property,OriginalValue,NewValue");
                writer.WriteLine($"Title,\"{EscapeCsv(origTitle)}\",\"{EscapeCsv(newTitle)}\"");
                writer.WriteLine($"Author,\"{EscapeCsv(origAuthor)}\",\"{EscapeCsv(newAuthor)}\"");
                writer.WriteLine($"Subject,\"{EscapeCsv(origSubject)}\",\"{EscapeCsv(newSubject)}\"");
                writer.WriteLine($"Keywords,\"{EscapeCsv(origKeywords)}\",\"{EscapeCsv(newKeywords)}\"");
                writer.WriteLine($"Creator,\"{EscapeCsv(origCreator)}\",\"{EscapeCsv(newCreator)}\"");
                writer.WriteLine($"CreationDate,\"{EscapeCsv(origCreationDate)}\",\"{EscapeCsv(newCreationDate)}\"");
                writer.WriteLine($"ModDate,\"{EscapeCsv(origModDate)}\",\"{EscapeCsv(newModDate)}\"");
            }

            Console.WriteLine($"Metadata audit written to '{csvPath}'. Updated PDF saved as '{outputPdf}'.");
        }
    }

    // Helper to escape double quotes for CSV compliance
    static string EscapeCsv(string input)
    {
        if (input == null) return string.Empty;
        return input.Replace("\"", "\"\"");
    }
}
