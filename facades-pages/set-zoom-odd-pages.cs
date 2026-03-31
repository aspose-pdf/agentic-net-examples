using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    // Build an int[] containing the 1‑based page numbers of all odd pages
                    List<int> oddPageList = new List<int>();
                    int pageCount = doc.Pages.Count;
                    for (int i = 1; i <= pageCount; i += 2)
                    {
                        oddPageList.Add(i);
                    }
                    editor.ProcessPages = oddPageList.ToArray(); // int[] required, not Hashtable

                    // Zoom expects an integer representing percentage (e.g., 120 = 120%)
                    editor.Zoom = 120; // 1.2 × original size

                    editor.ApplyChanges();
                }

                doc.Save(outputPath);
                Console.WriteLine($"Zoom applied to odd pages and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}
