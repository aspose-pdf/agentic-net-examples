using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the output PDF that will contain only pages 3, 8 and 12
        const string outputPath = "stream_specific.pdf";

        // Example: obtain the source PDF as a stream (could be from a file, network, etc.)
        // Here we simply read an existing file into a MemoryStream for demonstration.
        const string sourcePdfFile = "input.pdf";
        if (!File.Exists(sourcePdfFile))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfFile}");
            return;
        }

        using (FileStream sourceFileStream = File.OpenRead(sourcePdfFile))
        using (MemoryStream pdfStream = new MemoryStream())
        {
            sourceFileStream.CopyTo(pdfStream);
            pdfStream.Position = 0; // reset to beginning

            // Write the stream to a temporary file because PdfFileEditor works with file paths
            string tempPath = Path.GetTempFileName();
            try
            {
                File.WriteAllBytes(tempPath, pdfStream.ToArray());

                // Load the temporary PDF with Document to determine total page count
                using (Document doc = new Document(tempPath))
                {
                    int totalPages = doc.Pages.Count;

                    // Pages we want to keep (1‑based indexing)
                    HashSet<int> pagesToKeep = new HashSet<int> { 3, 8, 12 };

                    // Build a list of pages to delete (all pages except the ones we keep)
                    List<int> pagesToDelete = new List<int>();
                    for (int i = 1; i <= totalPages; i++)
                    {
                        if (!pagesToKeep.Contains(i))
                            pagesToDelete.Add(i);
                    }

                    // If there are no pages to delete, simply copy the temp file to the output
                    if (pagesToDelete.Count == 0)
                    {
                        File.Copy(tempPath, outputPath, overwrite: true);
                    }
                    else
                    {
                        // Use PdfFileEditor.Delete(sourcePath, pagesArray, outputPath)
                        PdfFileEditor editor = new PdfFileEditor();
                        editor.Delete(tempPath, pagesToDelete.ToArray(), outputPath);
                    }
                }

                Console.WriteLine($"Extracted pages saved to '{outputPath}'.");
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }
    }
}