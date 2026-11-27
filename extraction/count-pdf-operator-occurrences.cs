using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // for low‑level PDF operators

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string txtPath = "operator_summary.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Dictionary to store operator name -> occurrence count
        var operatorCounts = new Dictionary<string, int>(StringComparer.Ordinal);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Each page's Contents collection holds low‑level PDF operators
                foreach (Operator op in page.Contents)
                {
                    // The concrete operator class name (e.g., "Do", "Tm", "BT")
                    string name = op.GetType().Name;
                    if (operatorCounts.ContainsKey(name))
                        operatorCounts[name]++;
                    else
                        operatorCounts[name] = 1;
                }
            }
        }

        // Prepare the summary lines
        var lines = new List<string> { "Operator occurrences:" };
        foreach (var kvp in operatorCounts)
        {
            lines.Add($"{kvp.Key}: {kvp.Value}");
        }

        // Write the summary to a plain text file
        File.WriteAllLines(txtPath, lines);
        Console.WriteLine($"Operator summary written to '{txtPath}'.");
    }
}
