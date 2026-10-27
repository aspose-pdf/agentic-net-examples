using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp_landscape.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, rotate portrait pages to landscape, and save to a temporary file
        Document doc = new Document(inputPath);
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            // Rotate only if the page is portrait (height > width)
            if (page.PageInfo.Height > page.PageInfo.Width)
            {
                page.Rotate = Rotation.on90; // use the Rotation enum
            }
        }
        doc.Save(tempPath); // Save the resized PDF

        // Generate a booklet from the landscape PDF using the Facades API
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(tempPath, outputPath); // correct method name
        if (!success)
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }

        // Clean up the temporary file
        try { File.Delete(tempPath); } catch { }

        Console.WriteLine($"Booklet created at: {outputPath}");
    }
}