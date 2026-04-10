using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths of the split PDF files that need to be concatenated.
        // Example: { "split1.pdf", "split2.pdf", "split3.pdf" }
        string[] splitFiles = new string[]
        {
            "split1.pdf",
            "split2.pdf",
            "split3.pdf"
        };

        const string finalOutput = "merged.pdf";

        // Filter out files that do not exist and report them.
        var existingFiles = new System.Collections.Generic.List<string>();
        foreach (var file in splitFiles)
        {
            if (File.Exists(file))
            {
                existingFiles.Add(file);
            }
            else
            {
                Console.Error.WriteLine($"Warning: Input file '{file}' not found and will be skipped.");
            }
        }

        if (existingFiles.Count == 0)
        {
            Console.Error.WriteLine("No valid input PDF files were found. Exiting.");
            return;
        }

        // Use Aspose.Pdf.Document.Merge overload to concatenate the PDFs.
        // Start with the first existing document as the base.
        Document mergedDoc = null;
        try
        {
            mergedDoc = new Document(existingFiles[0]);
            for (int i = 1; i < existingFiles.Count; i++)
            {
                using (Document nextDoc = new Document(existingFiles[i]))
                {
                    mergedDoc.Merge(nextDoc);
                }
            }

            // Save the merged result.
            if (File.Exists(finalOutput))
                File.Delete(finalOutput);
            mergedDoc.Save(finalOutput);
            Console.WriteLine($"Merged PDF saved to '{finalOutput}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while merging PDFs: {ex.Message}");
        }
        finally
        {
            mergedDoc?.Dispose();
        }
    }
}
