using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string folderPath = "PdfFolder";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Change current directory to the folder so that Save uses simple filenames
        Directory.SetCurrentDirectory(folderPath);

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");

        foreach (string pdfFilePath in pdfFiles)
        {
            // Get only the file name (no path)
            string pdfFileName = Path.GetFileName(pdfFilePath);

            using (Document pdfDocument = new Document(pdfFileName))
            {
                if (pdfDocument.Pages.Count > 0)
                {
                    Page firstPage = pdfDocument.Pages[1];
                    // Rotate the first page by 90 degrees using the correct API
                    firstPage.Rotate = Rotation.on90;

                    string outputFileName = Path.GetFileNameWithoutExtension(pdfFileName) + "_rotated.pdf";
                    pdfDocument.Save(outputFileName);
                    Console.WriteLine($"Rotated first page: {pdfFileName} -> {outputFileName}");
                }
                else
                {
                    Console.WriteLine($"No pages in: {pdfFileName}");
                }
            }
        }
    }
}
