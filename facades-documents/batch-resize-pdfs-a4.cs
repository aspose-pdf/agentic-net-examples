using System;
using System.IO;
using Aspose.Pdf;

public class BatchResizeA4
{
    public static void Main()
    {
        const string sourceFolder = "source_pdfs";
        const string targetFolder = "resized_pdfs";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }

        // Change current directory to target folder so that Save uses a simple file name
        Directory.SetCurrentDirectory(targetFolder);

        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            try
            {
                using (Document pdfDocument = new Document(sourcePath))
                {
                    // Resize each page to A4 using width/height overload
                    double a4Width = PageSize.A4.Width;
                    double a4Height = PageSize.A4.Height;

                    for (int i = 1; i <= pdfDocument.Pages.Count; i++)
                    {
                        Page page = pdfDocument.Pages[i];
                        page.SetPageSize(a4Width, a4Height);
                    }

                    string outputFileName = Path.GetFileNameWithoutExtension(sourcePath) + ".pdf";
                    pdfDocument.Save(outputFileName);
                    Console.WriteLine($"Resized and saved: {outputFileName}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }
}
