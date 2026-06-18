using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace PdfFolderMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the folder to monitor (relative to the executable)
            string folderPath = "watched";
            Directory.CreateDirectory(folderPath);

            // Create a sample PDF in the folder (self‑contained example requirement)
            string samplePdfPath = Path.Combine(folderPath, "sample.pdf");
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save(samplePdfPath);
            }

            // Simple monitoring: scan the folder once and process any PDF files found
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
            foreach (string pdfPath in pdfFiles)
            {
                // Open the PDF, add a text fragment, and overwrite the file
                using (Document doc = new Document(pdfPath))
                {
                    TextFragment fragment = new TextFragment("Processed by service");
                    fragment.Position = new Position(100, 500);
                    // FontSize is set via TextState in Aspose.Pdf
                    fragment.TextState.FontSize = 12;
                    doc.Pages[1].Paragraphs.Add(fragment);
                    doc.Save(pdfPath);
                }
            }
        }
    }
}
