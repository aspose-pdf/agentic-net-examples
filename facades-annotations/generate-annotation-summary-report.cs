using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationSummaryUtility
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs (first argument or current directory)
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        // Output report file
        string reportPath = Path.Combine(inputFolder, "AnnotationReport.txt");

        // Prepare the report writer
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            writer.WriteLine("Annotation Summary Report");
            writer.WriteLine($"Generated on: {DateTime.Now}");
            writer.WriteLine(new string('=', 40));
            writer.WriteLine();

            // Process each PDF file in the folder
            foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                writer.WriteLine($"File: {Path.GetFileName(pdfFile)}");

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFile))
                {
                    // Dictionary to hold counts per annotation type
                    Dictionary<string, int> typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    // Iterate through pages and collect annotation types – works for all supported Aspose.Pdf versions
                    for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
                    {
                        Page page = doc.Pages[i];
                        foreach (Annotation annot in page.Annotations)
                        {
                            // Use the strongly‑typed AnnotationType enum; fallback to "Unknown" if needed
                            string typeName = annot.AnnotationType != AnnotationType.Unknown
                                ? annot.AnnotationType.ToString()
                                : "Unknown";

                            if (typeCounts.ContainsKey(typeName))
                                typeCounts[typeName]++;
                            else
                                typeCounts[typeName] = 1;
                        }
                    }

                    // Write the counts to the report
                    if (typeCounts.Count == 0)
                    {
                        writer.WriteLine("  No annotations found.");
                    }
                    else
                    {
                        foreach (var kvp in typeCounts)
                        {
                            writer.WriteLine($"  {kvp.Key}: {kvp.Value}");
                        }
                    }
                }

                writer.WriteLine(); // separate entries
            }

            writer.WriteLine("Report generation completed.");
        }

        Console.WriteLine($"Annotation summary written to: {reportPath}");
    }
}
