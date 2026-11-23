using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "attachments.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        var attachmentNames = new List<string>();

        // Load PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // EmbeddedFiles collection uses 1‑based indexing
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
            {
                var embeddedFile = doc.EmbeddedFiles[i];
                if (!string.IsNullOrEmpty(embeddedFile.Name))
                {
                    attachmentNames.Add(embeddedFile.Name);
                }
            }
        }

        // Create a comma‑separated list and write to a text file
        string csv = string.Join(",", attachmentNames);
        File.WriteAllText(outputTxt, csv);
        Console.WriteLine($"Attachment names saved to '{outputTxt}'.");
    }
}