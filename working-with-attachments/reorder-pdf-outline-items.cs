using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "reordered_portfolio.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document actually contains a portfolio (collection of embedded files)
            if (doc.Collection == null || doc.Collection.Count == 0)
            {
                Console.WriteLine("No portfolio items found. Saving original document.");
                doc.Save(outputPath);
                return;
            }

            // Capture current portfolio items in a list
            List<FileSpecification> currentItems = new List<FileSpecification>();
            foreach (FileSpecification spec in doc.Collection)
                currentItems.Add(spec);

            // Define the desired order. Example: reverse the existing order.
            currentItems.Reverse();

            // Remove all existing items from the portfolio collection using Delete (Clear is not available)
            for (int i = doc.Collection.Count; i >= 1; i--)
            {
                var spec = doc.Collection[i];
                if (spec != null && !string.IsNullOrEmpty(spec.Name))
                {
                    doc.Collection.Delete(spec.Name);
                }
            }

            // Re‑add items in the new sequence
            foreach (FileSpecification spec in currentItems)
                doc.Collection.Add(spec);

            // Save the reordered PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio reordered and saved to '{outputPath}'.");
    }
}
