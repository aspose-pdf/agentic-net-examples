using System;
using System.IO;
using Aspose.Pdf.Facades;

public class InsertPagesExample
{
    public static void Main()
    {
        const string destPath = "dest.pdf";
        const string srcPath = "source.pdf";

        if (!File.Exists(destPath))
        {
            Console.Error.WriteLine("Destination file not found: " + destPath);
            return;
        }

        if (!File.Exists(srcPath))
        {
            Console.Error.WriteLine("Source file not found: " + srcPath);
            return;
        }

        // Load destination PDF into memory
        using (MemoryStream destStream = new MemoryStream(File.ReadAllBytes(destPath)))
        // Load source PDF into memory
        using (MemoryStream srcStream = new MemoryStream(File.ReadAllBytes(srcPath)))
        // Output stream for the resulting PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            int insertLocation = 1; // 1‑based position after which pages will be inserted
            int[] pagesToInsert = new int[] { 2, 3 }; // pages from source PDF to insert

            bool success = editor.Insert(destStream, insertLocation, srcStream, pagesToInsert, outputStream);

            if (!success)
            {
                Console.Error.WriteLine("Insert operation failed.");
                return;
            }

            // Reset the output stream position before reading
            outputStream.Position = 0;

            // Write the combined PDF to a file (optional demonstration)
            const string resultPath = "result.pdf";
            File.WriteAllBytes(resultPath, outputStream.ToArray());
            Console.WriteLine("Pages inserted successfully. Result saved to " + resultPath);
        }
    }
}