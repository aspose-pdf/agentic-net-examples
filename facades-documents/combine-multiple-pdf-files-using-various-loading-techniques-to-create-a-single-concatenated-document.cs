using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class PdfConcatenationDemo
{
    static void Main()
    {
        try
        {
            // Input PDF files (ensure these files exist in the working directory)
            string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
            string outputPath = "merged_output.pdf";

            // -----------------------------------------------------------------
            // Helper: create simple PDFs if they are missing so the demo runs
            // -----------------------------------------------------------------
            foreach (var file in pdfFiles)
            {
                if (!File.Exists(file))
                {
                    // Creating PDFs with Aspose.Pdf may require GDI+ on non‑Windows platforms.
                    // Skip placeholder creation on those platforms to avoid the libgdiplus issue.
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        CreatePlaceholderPdf(file);
                    }
                    else
                    {
                        Console.WriteLine($"Placeholder PDF creation skipped on non‑Windows platform for {file}.");
                    }
                }
            }

            // ------------------------------------------------------------
            // 1. Concatenate using file paths (string[] overload)
            // ------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();
            // The method writes the result directly to the output file.
            editor.Concatenate(pdfFiles, outputPath);
            Console.WriteLine("Concatenated using file paths: " + outputPath);

            // ------------------------------------------------------------
            // 2. Concatenate using streams (Stream[] overload)
            // ------------------------------------------------------------
            // Open each source file as a read‑only stream.
            using (FileStream outStream = new FileStream("merged_streams.pdf", FileMode.Create, FileAccess.Write))
            {
                // Create an array of input streams.
                Stream[] inputStreams = new Stream[pdfFiles.Length];
                for (int i = 0; i < pdfFiles.Length; i++)
                {
                    inputStreams[i] = new FileStream(pdfFiles[i], FileMode.Open, FileAccess.Read);
                }

                // Ensure streams are closed after the operation.
                editor.CloseConcatenatedStreams = true;

                // Perform concatenation.
                editor.Concatenate(inputStreams, outStream);
                Console.WriteLine("Concatenated using streams: merged_streams.pdf");

                // Input streams will be closed automatically because CloseConcatenatedStreams = true.
            }

            // ------------------------------------------------------------
            // 3. Concatenate two PDFs using individual streams
            // ------------------------------------------------------------
            using (FileStream first = new FileStream("file1.pdf", FileMode.Open, FileAccess.Read))
            using (FileStream second = new FileStream("file2.pdf", FileMode.Open, FileAccess.Read))
            using (FileStream result = new FileStream("merged_two_streams.pdf", FileMode.Create, FileAccess.Write))
            {
                editor.CloseConcatenatedStreams = true;
                editor.Concatenate(first, second, result);
                Console.WriteLine("Concatenated two PDFs using individual streams: merged_two_streams.pdf");
            }

            // ------------------------------------------------------------
            // 4. Concatenate using Document objects (Document[] overload)
            // ------------------------------------------------------------
            // Load each PDF into a Document inside a using block for deterministic disposal.
            Document[] docs = new Document[pdfFiles.Length];
            try
            {
                for (int i = 0; i < pdfFiles.Length; i++)
                {
                    docs[i] = new Document(pdfFiles[i]);
                }

                // Destination document must be created before concatenation.
                using (Document dest = new Document())
                {
                    // Concatenate the source documents into the destination document.
                    editor.Concatenate(docs, dest);
                    // Save the resulting document.
                    dest.Save("merged_documents.pdf");
                    Console.WriteLine("Concatenated using Document objects: merged_documents.pdf");
                }
            }
            finally
            {
                // Ensure all source Document instances are disposed.
                foreach (var doc in docs)
                {
                    doc?.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during PDF concatenation: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a very simple one‑page PDF containing a text paragraph.
    /// This method is used only when the expected source files are missing,
    /// allowing the demo to run without external resources.
    /// </summary>
    private static void CreatePlaceholderPdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment($"Placeholder for {Path.GetFileName(path)}"));
            doc.Save(path);
        }
    }
}
