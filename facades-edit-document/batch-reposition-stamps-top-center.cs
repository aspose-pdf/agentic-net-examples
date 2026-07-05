using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where modified PDFs will be written
        const string outputFolder = "OutputPdfs";

        // Ensure both folders exist – creates them if they are missing
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder (if any)
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the document and bind it to the content editor
            using (Document doc = new Document(inputPath))
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Iterate pages – Aspose.Pdf uses 1‑based indexing
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Get all stamps present on the current page
                    StampInfo[] stamps = editor.GetStamps(pageNum);
                    if (stamps == null || stamps.Length == 0) continue;

                    // Retrieve page dimensions
                    double pageWidth  = doc.Pages[pageNum].PageInfo.Width;
                    double pageHeight = doc.Pages[pageNum].PageInfo.Height;

                    // Desired new position: top‑center of the page
                    double newX = pageWidth / 2.0;      // horizontal centre
                    double newY = pageHeight - 20.0;    // 20 points below the top edge

                    // Move each stamp to the calculated position
                    // MoveStamp expects a 1‑based stamp index
                    for (int i = 0; i < stamps.Length; i++)
                    {
                        editor.MoveStamp(pageNum, i + 1, newX, newY);
                    }
                }

                // Persist the changes
                editor.Save(outputPath);
            }

            Console.WriteLine($"Repositioned stamps saved to '{outputPath}'.");
        }
    }
}
