using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "matrix_data.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare the output text file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over low‑level content operators of the page
                    foreach (object op in page.Contents)
                    {
                        // Use reflection to avoid compile‑time dependency on low‑level operator types
                        Type opType = op.GetType();
                        if (opType.Name == "ConcatenateMatrixOperator")
                        {
                            // The matrix components are exposed as properties A, B, C, D, E, F
                            double a = (double)opType.GetProperty("A").GetValue(op);
                            double b = (double)opType.GetProperty("B").GetValue(op);
                            double c = (double)opType.GetProperty("C").GetValue(op);
                            double d = (double)opType.GetProperty("D").GetValue(op);
                            double e = (double)opType.GetProperty("E").GetValue(op);
                            double f = (double)opType.GetProperty("F").GetValue(op);

                            writer.WriteLine($"Page {pageIndex}: cm [{a} {b} {c} {d} {e} {f}]");
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Graphic state matrices have been written to '{outputPath}'.");
    }
}
