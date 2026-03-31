using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string csvPath = "metadata_audit.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        string originalTitle;
        string originalAuthor;
        string originalSubject;
        string originalKeywords;

        using (Document doc = new Document(inputPath))
        {
            DocumentInfo info = doc.Info;

            originalTitle = info.Title;
            originalAuthor = info.Author;
            originalSubject = info.Subject;
            originalKeywords = info.Keywords;

            // Set new metadata values
            string newTitle = "New Title";
            string newAuthor = "New Author";
            string newSubject = "New Subject";
            string newKeywords = "New, Keywords";

            info.Title = newTitle;
            info.Author = newAuthor;
            info.Subject = newSubject;
            info.Keywords = newKeywords;

            doc.Save(outputPath);
        }

        // Write audit CSV
        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            writer.WriteLine("Property,Original,New");
            writer.WriteLine("Title,\"" + (originalTitle ?? string.Empty).Replace("\"", "\"\"") + "\",\"New Title\"");
            writer.WriteLine("Author,\"" + (originalAuthor ?? string.Empty).Replace("\"", "\"\"") + "\",\"New Author\"");
            writer.WriteLine("Subject,\"" + (originalSubject ?? string.Empty).Replace("\"", "\"\"") + "\",\"New Subject\"");
            writer.WriteLine("Keywords,\"" + (originalKeywords ?? string.Empty).Replace("\"", "\"\"") + "\",\"New, Keywords\"");
        }

        Console.WriteLine("Metadata audit written to " + csvPath);
    }
}
