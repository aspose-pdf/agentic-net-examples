using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define the files to be merged
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputFile = "merged.pdf";

        Console.WriteLine("Starting PDF concatenation:");

        // ---------------------------------------------------------------------
        // 1. Validate input files and load them as Document objects.
        //    Each step is logged so the user can see which file is being processed.
        // ---------------------------------------------------------------------
        var documents = new List<Document>();
        foreach (var input in inputFiles)
        {
            Console.WriteLine($"Processing input file: {input}");
            if (!File.Exists(input))
            {
                Console.WriteLine($"Error: File not found – {input}");
                continue; // skip missing files (or you could return to abort)
            }

            try
            {
                var doc = new Document(input);
                documents.Add(doc);
                Console.WriteLine($"Loaded '{input}' successfully (Pages: {doc.Pages.Count}).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load '{input}': {ex.Message}");
            }
        }

        if (documents.Count == 0)
        {
            Console.WriteLine("No valid input files were found. Exiting.");
            return;
        }

        // ---------------------------------------------------------------------
        // 2. Merge the loaded documents using the Document.Merge overload.
        //    This approach preserves logical structure and outlines automatically.
        // ---------------------------------------------------------------------
        using (var merged = new Document())
        {
            // Add pages from the first document (if any) to initialise the target.
            merged.Pages.Add(documents[0].Pages);

            // Merge the remaining documents one‑by‑one, logging each operation.
            for (int i = 1; i < documents.Count; i++)
            {
                Console.WriteLine($"Merging '{inputFiles[i]}' into the output document.");
                merged.Pages.Add(documents[i].Pages);
            }

            // Log the output file name before saving.
            Console.WriteLine($"Saving merged document to: {outputFile}");
            try
            {
                merged.Save(outputFile);
                Console.WriteLine("Concatenation completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save merged file: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------
        // 3. Clean‑up: dispose all temporary Document instances.
        // ---------------------------------------------------------------------
        foreach (var doc in documents)
        {
            doc.Dispose();
        }
    }
}
