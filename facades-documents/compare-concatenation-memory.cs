using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create two temporary PDF files so that the file‑path overload has
        //    something to work with.  The PDFs are tiny – a single blank page –
        //    but they are valid and exist on disk.
        // ---------------------------------------------------------------------
        string firstPdf = CreateTempPdf();
        string secondPdf = CreateTempPdf();

        // Output files (simple filenames in the current directory)
        string outputPathFile = "concatenated_path.pdf";
        string outputPathStream = "concatenated_stream.pdf";

        try
        {
            // -----------------------------------------------------------------
            // 2. Measure memory for the file‑path overload
            // -----------------------------------------------------------------
            long memoryBeforePath = GC.GetTotalMemory(true);
            // PdfFileEditor does NOT implement IDisposable – instantiate directly.
            PdfFileEditor fileEditorPath = new PdfFileEditor();
            fileEditorPath.CloseConcatenatedStreams = true;
            bool resultPath = fileEditorPath.Concatenate(firstPdf, secondPdf, outputPathFile);
            long memoryAfterPath = GC.GetTotalMemory(true);
            Console.WriteLine("File‑path overload result: " + resultPath);
            Console.WriteLine("Memory used (bytes) by file‑path overload: " + (memoryAfterPath - memoryBeforePath));

            // -----------------------------------------------------------------
            // 3. Measure memory for the stream overload
            // -----------------------------------------------------------------
            long memoryBeforeStream = GC.GetTotalMemory(true);
            using (FileStream stream1 = new FileStream(firstPdf, FileMode.Open, FileAccess.Read))
            using (FileStream stream2 = new FileStream(secondPdf, FileMode.Open, FileAccess.Read))
            using (FileStream outStream = new FileStream(outputPathStream, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable – instantiate directly.
                PdfFileEditor fileEditorStream = new PdfFileEditor();
                fileEditorStream.CloseConcatenatedStreams = true;
                bool resultStream = fileEditorStream.Concatenate(stream1, stream2, outStream);
                long memoryAfterStream = GC.GetTotalMemory(true);
                Console.WriteLine("Stream overload result: " + resultStream);
                Console.WriteLine("Memory used (bytes) by stream overload: " + (memoryAfterStream - memoryBeforeStream));
            }
        }
        finally
        {
            // -----------------------------------------------------------------
            // 4. Clean up temporary files – the concatenated results are left on
            //    disk so the user can inspect them.
            // -----------------------------------------------------------------
            SafeDelete(firstPdf);
            SafeDelete(secondPdf);
        }
    }

    // ---------------------------------------------------------------------
    // Helper: creates a one‑page PDF in the system's temp folder and returns
    // the full path.  The file is automatically deleted by the caller.
    // ---------------------------------------------------------------------
    private static string CreateTempPdf()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        using (Document doc = new Document())
        {
            // Add a single blank page – enough for concatenation tests.
            doc.Pages.Add();
            doc.Save(tempPath);
        }
        return tempPath;
    }

    // ---------------------------------------------------------------------
    // Helper: deletes a file if it exists, swallowing any exception so that
    // the demo never crashes during cleanup.
    // ---------------------------------------------------------------------
    private static void SafeDelete(string path)
    {
        try
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        catch
        {
            // Ignored – cleanup should not interrupt the demo.
        }
    }
}
