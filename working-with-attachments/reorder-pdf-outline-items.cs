using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class ReorderPortfolio
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered_portfolio.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired order of portfolio items (file names as they appear in the PDF)
        string[] desiredOrder = new string[]
        {
            "Attachment2.pdf",
            "Attachment1.pdf",
            "Attachment3.pdf"
        };

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Ensure the PDF actually contains a portfolio collection
                if (doc.Collection == null)
                {
                    Console.WriteLine("The PDF does not contain a portfolio collection.");
                    return;
                }

                // Build a lookup of existing portfolio items (FileSpecification) by their name
                var existing = new Dictionary<string, FileSpecification>(StringComparer.OrdinalIgnoreCase);
                foreach (FileSpecification spec in doc.Collection)
                {
                    // Clone the specification so we can safely clear the original collection later
                    var clone = new FileSpecification(spec.Name, spec.Description);
                    clone.MIMEType = spec.MIMEType;
                    clone.AFRelationship = spec.AFRelationship;

                    if (spec.Contents != null)
                    {
                        var ms = new MemoryStream();
                        spec.Contents.Position = 0;
                        spec.Contents.CopyTo(ms);
                        ms.Position = 0;
                        clone.Contents = ms;
                    }

                    existing[spec.Name] = clone;
                }

                // Replace the current collection with a new empty one
                doc.Collection = new Collection();

                // Re‑add items following the desired sequence
                foreach (string name in desiredOrder)
                {
                    if (existing.TryGetValue(name, out var spec))
                    {
                        doc.Collection.Add(spec);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: portfolio item \"{name}\" not found in source PDF.");
                    }
                }

                // Optionally keep any items that were not mentioned in desiredOrder
                foreach (var kvp in existing)
                {
                    if (Array.IndexOf(desiredOrder, kvp.Key) < 0)
                    {
                        doc.Collection.Add(kvp.Value);
                    }
                }

                // Save the reordered PDF portfolio
                doc.Save(outputPath);
            }

            Console.WriteLine($"Portfolio reordered and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
