using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Example regex: allow alphanumeric characters and spaces, 1‑100 characters long
        const string metaPattern = @"^[A-Za-z0-9\s]{1,100}$";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata via PdfFileInfo facade
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Title
            string title = "Sample Document Title";
            if (Regex.IsMatch(title, metaPattern))
                info.Title = title;
            else
                Console.WriteLine("Title does not match the required pattern.");

            // Author
            string author = "John Doe";
            if (Regex.IsMatch(author, metaPattern))
                info.Author = author;
            else
                Console.WriteLine("Author does not match the required pattern.");

            // Subject
            string subject = "Demo Subject";
            if (Regex.IsMatch(subject, metaPattern))
                info.Subject = subject;
            else
                Console.WriteLine("Subject does not match the required pattern.");

            // Keywords (comma‑separated alphanumeric tokens)
            string keywords = "keyword1,keyword2,keyword3";
            const string keywordsPattern = @"^([A-Za-z0-9]+,?)*$";
            if (Regex.IsMatch(keywords, keywordsPattern))
                info.Keywords = keywords;
            else
                Console.WriteLine("Keywords do not match the required pattern.");

            // Persist changes to a new PDF file
            info.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata validated and saved to '{outputPath}'.");
    }
}