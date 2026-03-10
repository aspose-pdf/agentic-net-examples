using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to load
        string[] pdfFiles = { "sample1.pdf", "sample2.pdf", "sample3.pdf" };

        // Collection to hold the loaded Document objects in memory
        List<Document> loadedDocuments = new List<Document>();

        foreach (string filePath in pdfFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Load the PDF directly into a Document instance.
            // The Document constructor reads the file and keeps the content in memory.
            Document doc = new Document(filePath);
            loadedDocuments.Add(doc);

            Console.WriteLine($"Loaded '{Path.GetFileName(filePath)}' into memory.");
        }

        // At this point, 'loadedDocuments' contains independent Document objects
        // that are fully loaded in memory and can be processed further.
        Console.WriteLine($"Total documents loaded: {loadedDocuments.Count}");
    }
}
