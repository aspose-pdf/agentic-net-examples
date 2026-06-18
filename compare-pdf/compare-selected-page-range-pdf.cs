using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const int startPage = 2; // inclusive
        const int endPage   = 4; // inclusive

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents using the core Document API (lifecycle managed by using)
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create comparison options
            ComparisonOptions options = new ComparisonOptions();

            // The ComparisonOptions class does not expose StartPage/EndPage in all versions.
            // Use reflection to set them if they exist; otherwise the whole documents will be compared.
            PropertyInfo startProp = typeof(ComparisonOptions).GetProperty("StartPage");
            PropertyInfo endProp   = typeof(ComparisonOptions).GetProperty("EndPage");

            if (startProp != null && endProp != null)
            {
                startProp.SetValue(options, startPage);
                endProp.SetValue(options, endPage);
            }
            else
            {
                Console.WriteLine("StartPage/EndPage properties are not available in this version; comparing full documents.");
            }

            // Perform page‑by‑page text comparison
            List<List<DiffOperation>> differences = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Output a simple summary of the comparison results
            for (int i = 0; i < differences.Count; i++)
            {
                Console.WriteLine($"Page {i + 1}: {differences[i].Count} differences detected.");
            }
        }
    }
}