using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge (order matters)
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
        const string outputPdf = "merged_output.pdf";

        // Verify that all input files exist
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        try
        {
            // Load the first PDF as the base document
            using (Document mergedDoc = new Document(pdfFiles[0]))
            {
                // Append each subsequent PDF to the base document
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    using (Document srcDoc = new Document(pdfFiles[i]))
                    {
                        mergedDoc.Pages.Add(srcDoc.Pages);
                    }
                }

                // Save the merged result
                mergedDoc.Save(outputPdf);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}