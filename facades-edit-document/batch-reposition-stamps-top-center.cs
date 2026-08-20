using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_repositioned.pdf");

            // Load the document to obtain page dimensions (using rule for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Initialize the content editor and bind the same PDF file
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(inputPath);

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Retrieve page size
                    var pageInfo = doc.Pages[pageNum].PageInfo;
                    double pageWidth = pageInfo.Width;
                    double pageHeight = pageInfo.Height;

                    // Get all stamps on the current page
                    StampInfo[] stamps = editor.GetStamps(pageNum);
                    if (stamps == null) continue;

                    // Reposition each stamp to top‑center
                    for (int i = 0; i < stamps.Length; i++)
                    {
                        // Horizontal center, 10 points below the top edge
                        double newX = pageWidth / 2.0;
                        double newY = pageHeight - 10.0;

                        // MoveStamp uses 1‑based stamp index
                        editor.MoveStamp(pageNum, i + 1, newX, newY);
                    }
                }

                // Save the modified PDF
                editor.Save(outputPath);
                editor.Close();
            }

            Console.WriteLine($"Repositioned PDF saved to '{outputPath}'.");
        }
    }
}