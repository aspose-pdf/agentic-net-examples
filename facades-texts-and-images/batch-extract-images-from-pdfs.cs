using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Runtime.Versioning;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Temporary folder for single‑page PDFs
        const string tempFolder = "TempPages";
        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Ensure all required directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(tempFolder);
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Split the PDF into single‑page PDFs.
            // %NUM% in the template is replaced with the page number (1‑based).
            string pageTemplate = Path.Combine(tempFolder, $"{pdfBaseName}_page%NUM%.pdf");
            var editor = new PdfFileEditor();
            editor.SplitToPages(pdfPath, pageTemplate);

            // Iterate over the generated single‑page PDFs.
            foreach (string pagePdf in Directory.GetFiles(tempFolder, $"{pdfBaseName}_page*.pdf"))
            {
                int pageNumber = ExtractPageNumber(pagePdf); // 1‑based page index

                // Extract all images from the current page PDF.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pagePdf);
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Build the output file name: <pdfname>_page<page>_img<index>.png
                        string outFile = Path.Combine(
                            outputFolder,
                            $"{pdfBaseName}_page{pageNumber}_img{imageIndex}.png");

                        // Get the image as a memory stream (platform‑agnostic) and write it to disk.
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            extractor.GetNextImage(imgStream);
                            // Reset position before writing.
                            imgStream.Position = 0;
                            using (FileStream fs = new FileStream(outFile, FileMode.Create, FileAccess.Write))
                            {
                                imgStream.CopyTo(fs);
                            }
                        }
                        imageIndex++;
                    }
                }

                // Clean up the temporary single‑page PDF.
                File.Delete(pagePdf);
            }
        }
    }

    // Helper to parse the page number from a file name like "doc_page3.pdf"
    static int ExtractPageNumber(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        int marker = fileName.LastIndexOf("_page", StringComparison.OrdinalIgnoreCase);
        if (marker >= 0)
        {
            string numberPart = fileName.Substring(marker + 5);
            if (int.TryParse(numberPart, out int page))
                return page;
        }
        return 0; // fallback if parsing fails
    }
}
