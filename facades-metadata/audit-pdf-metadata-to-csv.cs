using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string csvPath   = "metadata_audit.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // ----- Read original metadata using the Facade -----
        PdfFileInfo originalInfo = new PdfFileInfo(inputPdf);
        string origTitle    = originalInfo.Title;
        string origAuthor   = originalInfo.Author;
        string origSubject  = originalInfo.Subject;
        string origKeywords = originalInfo.Keywords;
        string origCreator  = originalInfo.Creator;
        string origProducer = originalInfo.Producer;

        // ----- Load the PDF, modify metadata, and save -----
        using (Document doc = new Document(inputPdf))
        {
            // Set new metadata values
            doc.Info.Title    = "New Title";
            doc.Info.Author   = "New Author";
            doc.Info.Subject  = "New Subject";
            doc.Info.Keywords = "new,keywords";
            doc.Info.Creator  = "MyApp";
            doc.Info.Producer = "Aspose.Pdf";

            // Save the updated document
            doc.Save(outputPdf);
        }

        // ----- Read new metadata after saving -----
        PdfFileInfo newInfo = new PdfFileInfo(outputPdf);
        string newTitle    = newInfo.Title;
        string newAuthor   = newInfo.Author;
        string newSubject  = newInfo.Subject;
        string newKeywords = newInfo.Keywords;
        string newCreator  = newInfo.Creator;
        string newProducer = newInfo.Producer;

        // ----- Write audit information to CSV -----
        using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
        {
            // CSV header
            writer.WriteLine("Property,Original,New");

            // Each metadata field
            writer.WriteLine($"Title,\"{Escape(origTitle)}\",\"{Escape(newTitle)}\"");
            writer.WriteLine($"Author,\"{Escape(origAuthor)}\",\"{Escape(newAuthor)}\"");
            writer.WriteLine($"Subject,\"{Escape(origSubject)}\",\"{Escape(newSubject)}\"");
            writer.WriteLine($"Keywords,\"{Escape(origKeywords)}\",\"{Escape(newKeywords)}\"");
            writer.WriteLine($"Creator,\"{Escape(origCreator)}\",\"{Escape(newCreator)}\"");
            writer.WriteLine($"Producer,\"{Escape(origProducer)}\",\"{Escape(newProducer)}\"");
        }

        Console.WriteLine($"Metadata audit written to {csvPath}");
    }

    // Helper to escape double quotes for CSV compliance
    static string Escape(string value)
    {
        if (value == null) return string.Empty;
        return value.Replace("\"", "\"\"");
    }
}