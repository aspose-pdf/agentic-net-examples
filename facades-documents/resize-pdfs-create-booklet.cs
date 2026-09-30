using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Final booklet PDF
        const string outputBooklet = "Booklet.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Temporary folder for resized PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "ResizedPdfTemp");
        Directory.CreateDirectory(tempFolder);

        // Collect paths of resized PDFs
        List<string> resizedPdfPaths = new List<string>();

        try
        {
            // Process each PDF in the input folder
            foreach (string srcPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(srcPath);
                string resizedPath = Path.Combine(tempFolder, fileName);

                // Load the document, resize each page, and save to a temporary file
                Document doc = new Document(srcPath);
                foreach (Page page in doc.Pages)
                {
                    // Set page size to 1024x768 points (1 point = 1/72 inch)
                    page.PageInfo.Width = 1024;
                    page.PageInfo.Height = 768;
                }
                doc.Save(resizedPath);
                resizedPdfPaths.Add(resizedPath);
            }

            // Concatenate all resized PDFs into a single booklet
            PdfFileEditor fileEditor = new PdfFileEditor();
            fileEditor.Concatenate(resizedPdfPaths.ToArray(), outputBooklet);

            Console.WriteLine($"Booklet created: {outputBooklet}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            foreach (string path in resizedPdfPaths)
            {
                try { File.Delete(path); } catch { /* ignore */ }
            }
            try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }
        }
    }
}
