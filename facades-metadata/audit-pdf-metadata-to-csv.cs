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

        // Open the PDF file for metadata manipulation
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Capture original metadata values
            string origTitle = pdfInfo.Title;
            string origAuthor = pdfInfo.Author;
            string origSubject = pdfInfo.Subject;
            string origKeywords = pdfInfo.Keywords;
            string origCreator = pdfInfo.Creator;
            string origCreationDate = pdfInfo.CreationDate ?? "";
            string origModDate = pdfInfo.ModDate ?? "";
            string origProducer = pdfInfo.Producer;

            // Set new metadata values (example updates)
            pdfInfo.Title = "New Document Title";
            pdfInfo.Author = "New Author";
            pdfInfo.Subject = "Updated Subject";
            pdfInfo.Keywords = "keyword1,keyword2";
            pdfInfo.Creator = "My Application";
            // PdfFileInfo expects PDF‑date formatted strings, not DateTime objects
            string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdfInfo.CreationDate = pdfDate;
            pdfInfo.ModDate = pdfDate;

            // Save the updated PDF
            bool saved = pdfInfo.SaveNewInfo(outputPdf);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save the updated PDF.");
                return;
            }

            // Capture new metadata values after the update
            string newTitle = pdfInfo.Title;
            string newAuthor = pdfInfo.Author;
            string newSubject = pdfInfo.Subject;
            string newKeywords = pdfInfo.Keywords;
            string newCreator = pdfInfo.Creator;
            string newCreationDate = pdfInfo.CreationDate ?? "";
            string newModDate = pdfInfo.ModDate ?? "";
            string newProducer = pdfInfo.Producer; // unchanged (read‑only)

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
                writer.WriteLine($"Producer,\"{EscapeCsv(origProducer)}\",\"{EscapeCsv(newProducer)}\"");
            }

            Console.WriteLine($"Metadata audit completed. CSV saved to '{csvPath}'.");
        }
    }

    // Helper method to escape double quotes for CSV compliance
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        return field.Replace("\"", "\"\"");
    }
}
