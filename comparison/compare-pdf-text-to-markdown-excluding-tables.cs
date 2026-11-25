using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputMd = "diff.md";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure comparison to ignore tables
                ComparisonOptions options = new ComparisonOptions
                {
                    ExcludeTables = true
                };

                // Perform flat text comparison; result is a list of DiffOperation objects
                var diffOperations = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

                // Convert the list of DiffOperation objects to a Markdown string
                string markdownDiff = string.Join(Environment.NewLine, diffOperations.Select(op => op.ToString()));

                // Write the Markdown diff to a file
                File.WriteAllText(outputMd, markdownDiff);
            }

            Console.WriteLine($"Markdown diff saved to '{outputMd}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
