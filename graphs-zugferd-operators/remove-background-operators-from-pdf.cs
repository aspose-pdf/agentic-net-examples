using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using the core Document API
            Document doc = new Document(inputPath);

            // Iterate through each page and delete unwanted operators from the content stream.
            // "Do" – external XObject drawing operator
            // "BI" – inline image operator
            foreach (Page page in doc.Pages)
            {
                OperatorCollection ops = page.Contents;
                // OperatorCollection is 1‑based; iterate backwards when removing items.
                for (int i = ops.Count; i >= 1; i--)
                {
                    var op = ops[i];
                    if (op is Do || op is BI)
                    {
                        ops.Delete(i); // Remove the operator at the current 1‑based index.
                    }
                }
            }

            // Save the cleaned PDF.
            doc.Save(outputPath);
            Console.WriteLine($"Background operators removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
