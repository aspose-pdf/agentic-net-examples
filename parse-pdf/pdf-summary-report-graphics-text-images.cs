using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // ---------- Count vector graphics ----------
                int graphicsCount = 0;
                foreach (Page page in doc.Pages)
                {
                    foreach (var paragraph in page.Paragraphs)
                    {
                        if (paragraph is Aspose.Pdf.Drawing.Graph)
                        {
                            graphicsCount++;
                        }
                    }
                }

                // ---------- Extract total text length ----------
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                int textLength = absorber.Text?.Length ?? 0;

                // ---------- Count images ----------
                int imageCount = 0;
                foreach (Page page in doc.Pages)
                {
                    foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                    {
                        imageCount++;
                    }
                }

                // ---------- Output summary ----------
                Console.WriteLine($"Report for '{pdfPath}':");
                Console.WriteLine($"  Graphics count: {graphicsCount}");
                Console.WriteLine($"  Total text length: {textLength}");
                Console.WriteLine($"  Image count: {imageCount}");
                Console.WriteLine();
            }
        }
    }
}