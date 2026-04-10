using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo provides access to PDF meta‑information.
        // Wrap it in a using block so that Close/Dispose is called automatically.
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            // Read existing metadata.
            Console.WriteLine($"Title : {fileInfo.Title}");
            Console.WriteLine($"Author: {fileInfo.Author}");
            Console.WriteLine($"Pages : {fileInfo.NumberOfPages}");

            // Modify metadata as needed.
            fileInfo.Title  = "Updated Title";
            fileInfo.Author = "John Doe";
            fileInfo.Subject = "Demo of PdfFileInfo";

            // Load the PDF document itself.
            // Document also implements IDisposable, so it must be disposed.
            using (Document doc = new Document(inputPath))
            {
                // Example operation on the document – add a blank page at the end.
                doc.Pages.Add();

                // Save the modified PDF (metadata changes are not yet persisted).
                doc.Save(outputPath);
            }

            // Persist the changed metadata back to the file.
            // SaveNewInfo writes the updated meta‑information into the specified file.
            fileInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}