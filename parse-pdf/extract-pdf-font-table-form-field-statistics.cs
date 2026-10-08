using System;
using System.IO;
using System.Linq; // Needed for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // List of PDF files to analyse
        string[] pdfFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf"
            // add more file names as needed
        };

        foreach (string filePath in pdfFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(filePath))
            {
                // ---------- Count fonts ----------
                int fontCount = 0;
                foreach (Page page in doc.Pages)
                {
                    // Each page has a Resources.Fonts collection
                    fontCount += page.Resources.Fonts.Count;
                }

                // ---------- Count tables ----------
                int tableCount = 0;
                foreach (Page page in doc.Pages)
                {
                    // Tables are stored as Paragraph objects of type Aspose.Pdf.Table
                    foreach (var paragraph in page.Paragraphs)
                    {
                        if (paragraph is Table)
                        {
                            tableCount++;
                        }
                    }
                }

                // ---------- Count form fields ----------
                int formFieldCount = 0;
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    // Form.Fields implements IEnumerable, use LINQ Count() extension
                    formFieldCount = doc.Form.Fields.Count();
                }

                // Output the statistics for the current document
                Console.WriteLine($"Document: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Fonts       : {fontCount}");
                Console.WriteLine($"  Tables      : {tableCount}");
                Console.WriteLine($"  Form fields : {formFieldCount}");
                Console.WriteLine();
            }
        }
    }
}
