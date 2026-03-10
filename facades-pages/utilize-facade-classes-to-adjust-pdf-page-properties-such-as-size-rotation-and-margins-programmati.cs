using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Adjust page size, rotation, and zoom using PdfPageEditor
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Load the source PDF
            pageEditor.BindPdf(inputPath);

            // Set the desired page size (A4)
            pageEditor.PageSize = PageSize.A4;

            // Rotate all pages by 90 degrees
            pageEditor.Rotation = 90;

            // Shift the content origin (optional)
            pageEditor.MovePosition(10, 20); // 10 points right, 20 points up

            // Set zoom to 90% (float literal required)
            pageEditor.Zoom = 0.9f;

            // Apply the changes to the document
            pageEditor.ApplyChanges();

            // Save the intermediate result to a temporary file
            string tempPath = Path.GetTempFileName();
            pageEditor.Save(tempPath);
            pageEditor.Close();

            // Add uniform margins to all pages using PdfFileEditor
            PdfFileEditor fileEditor = new PdfFileEditor();
            bool success = fileEditor.AddMargins(
                tempPath,               // source PDF
                outputPath,             // destination PDF
                null,                   // null processes all pages
                20,                     // left margin (points)
                20,                     // right margin (points)
                20,                     // top margin (points)
                20);                    // bottom margin (points)

            if (!success)
            {
                Console.Error.WriteLine("Failed to add margins.");
            }

            // Clean up the temporary file
            try { File.Delete(tempPath); } catch { }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
