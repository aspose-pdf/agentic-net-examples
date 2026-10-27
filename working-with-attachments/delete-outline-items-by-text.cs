using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_cleaned.pdf";
        const string matchText = "Obsolete Item Description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a portfolio (collection of embedded files)
            if (doc.Collection == null || doc.Collection.Count == 0)
            {
                Console.WriteLine("No portfolio items found.");
                doc.Save(outputPath);
                return;
            }

            // Gather the names of the specifications that match the description
            var namesToRemove = new List<string>();
            for (int i = 0; i < doc.Collection.Count; i++)
            {
                var fileSpec = doc.Collection[i];
                if (!string.IsNullOrEmpty(fileSpec.Description) &&
                    fileSpec.Description.IndexOf(matchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // The Delete method expects the attachment name (string), not the object itself
                    namesToRemove.Add(fileSpec.Name);
                }
            }

            // Remove the matched specifications using the string‑based Delete overload
            foreach (var name in namesToRemove)
            {
                doc.Collection.Delete(name);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio cleaned and saved to '{outputPath}'.");
    }
}
