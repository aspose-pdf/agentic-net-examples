using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SummaryReport
{
    static void Main(string[] args)
    {
        // If no arguments are provided, use a default PDF file name.
        string[] pdfFiles = args.Length > 0 ? args : new[] { "sample.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // ---------- Text extraction ----------
                // TextAbsorber extracts all text from the document.
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                // Total length of extracted text (character count).
                int totalTextLength = absorber.Text?.Length ?? 0;

                // ---------- Image counting ----------
                int totalImageCount = 0;
                foreach (Page page in doc.Pages)
                {
                    // page.Resources.Images is a collection of XImage objects.
                    totalImageCount += page.Resources.Images.Count;
                }

                // ---------- Graphics counting ----------
                // Aspose.Pdf does not expose a direct API for counting vector graphics.
                // As a placeholder, we set the count to zero. Replace this with
                // appropriate logic if a future version provides such an API.
                int totalGraphicsCount = 0;

                // ---------- Report ----------
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"PDF File          : {Path.GetFileName(pdfPath)}");
                Console.WriteLine($"Total Pages       : {doc.Pages.Count}");
                Console.WriteLine($"Extracted Graphics: {totalGraphicsCount}");
                Console.WriteLine($"Total Text Length : {totalTextLength} characters");
                Console.WriteLine($"Image Count       : {totalImageCount}");
                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}