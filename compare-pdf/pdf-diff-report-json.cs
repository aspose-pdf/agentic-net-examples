using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

namespace PdfDiffReport
{
    // Simple representation of a difference between two PDFs
    public class DiffOperation
    {
        public int PageNumber { get; set; }
        public string Description { get; set; }
    }

    // Generates a JSON string from a collection of DiffOperation objects
    public class JsonDiffOutputGenerator
    {
        public string GenerateOutput(IEnumerable<DiffOperation> diffOperations)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(diffOperations, options);
        }
    }

    class Program
    {
        static void Main()
        {
            const string firstPdfPath = "doc1.pdf";
            const string secondPdfPath = "doc2.pdf";
            const string jsonReportPath = "diff_report.json";

            // Verify that both source PDFs exist
            if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
            {
                Console.Error.WriteLine("One or both input PDF files were not found.");
                return;
            }

            // Load the PDFs inside using blocks for deterministic disposal
            using (Document firstDoc = new Document(firstPdfPath))
            using (Document secondDoc = new Document(secondPdfPath))
            {
                // Perform a very basic comparison – page count and page size differences
                var diffOperations = new List<DiffOperation>();

                int maxPages = Math.Max(firstDoc.Pages.Count, secondDoc.Pages.Count);
                for (int i = 1; i <= maxPages; i++)
                {
                    bool firstHasPage = i <= firstDoc.Pages.Count;
                    bool secondHasPage = i <= secondDoc.Pages.Count;

                    if (!firstHasPage)
                    {
                        diffOperations.Add(new DiffOperation
                        {
                            PageNumber = i,
                            Description = "Page missing in first document"
                        });
                        continue;
                    }

                    if (!secondHasPage)
                    {
                        diffOperations.Add(new DiffOperation
                        {
                            PageNumber = i,
                            Description = "Page missing in second document"
                        });
                        continue;
                    }

                    var page1 = firstDoc.Pages[i];
                    var page2 = secondDoc.Pages[i];

                    // Compare page dimensions (width & height)
                    if (page1.PageInfo.Width != page2.PageInfo.Width ||
                        page1.PageInfo.Height != page2.PageInfo.Height)
                    {
                        diffOperations.Add(new DiffOperation
                        {
                            PageNumber = i,
                            Description = "Page size differs"
                        });
                    }
                }

                // Generate a JSON representation of the differences
                JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
                string jsonReport = jsonGenerator.GenerateOutput(diffOperations);

                // Write the JSON report to a file
                File.WriteAllText(jsonReportPath, jsonReport);
                Console.WriteLine($"Diff report successfully saved to '{jsonReportPath}'.");
            }
        }
    }
}
