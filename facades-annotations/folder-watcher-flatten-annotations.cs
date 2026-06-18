using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFolderMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the folder to watch
            string watchFolder = "watchfolder";
            Directory.CreateDirectory(watchFolder);

            // Create a sample PDF in the folder (self‑contained example)
            string samplePdfPath = Path.Combine(watchFolder, "sample.pdf");
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save(samplePdfPath);
            }

            // Scan the folder for PDF files (limit to 4 files due to evaluation mode)
            string[] pdfFiles = Directory.GetFiles(watchFolder, "*.pdf");
            int processedCount = 0;
            foreach (string pdfFile in pdfFiles)
            {
                if (processedCount >= 4)
                {
                    break; // respect collection limit in evaluation mode
                }

                // Flatten annotations in the PDF
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfFile);
                    editor.FlatteningAnnotations();
                    string outputFile = Path.Combine(watchFolder, "flattened_" + Path.GetFileName(pdfFile));
                    editor.Save(outputFile);
                    editor.Close();
                }

                processedCount++;
            }

            Console.WriteLine("Processed " + processedCount + " PDF(s) in folder '" + watchFolder + "'.");
        }
    }
}