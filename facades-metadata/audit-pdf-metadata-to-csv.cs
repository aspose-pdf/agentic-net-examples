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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF metadata using PdfFileInfo (facade)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Capture original metadata values
            string origTitle = pdfInfo.Title;
            string origAuthor = pdfInfo.Author;
            string origSubject = pdfInfo.Subject;
            string origKeywords = pdfInfo.Keywords;
            string origCreator = pdfInfo.Creator;
            string origProducer = pdfInfo.Producer;
            // PdfFileInfo dates are strings in PDF‑date format, not DateTime?
            string origCreationDate = pdfInfo.CreationDate;
            string origModDate = pdfInfo.ModDate;

            // Update metadata as needed
            pdfInfo.Title = "New Title";
            pdfInfo.Author = "New Author";
            pdfInfo.Subject = "Updated Subject";
            pdfInfo.Keywords = "keyword1;keyword2";

            // Save the PDF with updated metadata
            bool saved = pdfInfo.SaveNewInfo(outputPdf);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
                return;
            }

            // Capture new metadata values (properties already reflect changes)
            string newTitle = pdfInfo.Title;
            string newAuthor = pdfInfo.Author;
            string newSubject = pdfInfo.Subject;
            string newKeywords = pdfInfo.Keywords;
            string newCreator = pdfInfo.Creator;
            string newProducer = pdfInfo.Producer;
            string newCreationDate = pdfInfo.CreationDate;
            string newModDate = pdfInfo.ModDate;

            // Write audit information to CSV
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                writer.WriteLine("Property,Original,New");

                void WriteLine(string property, string original, string updated)
                {
                    string o = original?.Replace("\"", "\"\"");
                    string u = updated?.Replace("\"", "\"\"");
                    writer.WriteLine($"{property},\"{o}\",\"{u}\"");
                }

                WriteLine("Title", origTitle, newTitle);
                WriteLine("Author", origAuthor, newAuthor);
                WriteLine("Subject", origSubject, newSubject);
                WriteLine("Keywords", origKeywords, newKeywords);
                WriteLine("Creator", origCreator, newCreator);
                WriteLine("Producer", origProducer, newProducer);
                WriteLine("CreationDate", origCreationDate, newCreationDate);
                WriteLine("ModDate", origModDate, newModDate);
            }

            Console.WriteLine($"Metadata audit written to '{csvPath}'.");
        }
    }
}
