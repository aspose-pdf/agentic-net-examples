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
        const string logPath = "audit.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Log page information before any edit
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                writer.WriteLine("=== BEFORE EDIT ===");
                LogPageInfo(editor, writer);
            }

            // Example edit: rotate all pages by 90 degrees
            editor.Rotation = 90;
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPath);

            // Log page information after the edit (before closing the editor)
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine("=== AFTER EDIT ===");
                LogPageInfo(editor, writer);
            }

            // Close the editor after logging
            editor.Close();
        }

        Console.WriteLine("Audit completed. Log saved to audit.txt");
    }

    static void LogPageInfo(PdfPageEditor editor, StreamWriter writer)
    {
        int pageCount = editor.GetPages();
        writer.WriteLine("Total pages: " + pageCount);
        for (int i = 1; i <= pageCount; i++)
        {
            // GetPageSize returns Aspose.Pdf.PageSize, not Rectangle
            Aspose.Pdf.PageSize pageSize = editor.GetPageSize(i);
            int rotation = editor.GetPageRotation(i);
            writer.WriteLine($"Page {i}: Width={pageSize.Width}, Height={pageSize.Height}, Rotation={rotation}");
        }
    }
}
