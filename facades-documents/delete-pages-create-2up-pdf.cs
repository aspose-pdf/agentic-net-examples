using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Example usage (uncomment to test)
        // using (var input = File.OpenRead("input.pdf"))
        // using (var output = new MemoryStream())
        // {
        //     DeletePagesAndApply2Up(input, output);
        //     File.WriteAllBytes("output.pdf", output.ToArray());
        // }
    }

    /// <summary>
    /// Deletes selected pages from the input PDF stream, then creates a 2‑up layout
    /// (two original pages per sheet) and writes the result to the output stream.
    /// </summary>
    /// <param name="inputPdf">Stream containing the source PDF (must be readable).</param>
    /// <param name="outputPdf">Stream that will receive the processed PDF (must be writable).</param>
    public static void DeletePagesAndApply2Up(Stream inputPdf, Stream outputPdf)
    {
        // Ensure the input stream is at the beginning.
        if (inputPdf.CanSeek)
            inputPdf.Position = 0;

        // 1. Copy the input stream to a temporary file because Aspose.Pdf.Document works with file paths.
        string tempInputPath = Path.GetTempFileName();
        using (var tempInputFile = new FileStream(tempInputPath, FileMode.Create, FileAccess.Write))
        {
            inputPdf.CopyTo(tempInputFile);
        }

        // 2. Load the PDF using Document (the recommended way for page manipulation).
        Document pdfDoc = new Document(tempInputPath);

        // 3. Delete the unwanted pages. Deleting in descending order prevents index shift.
        int[] pagesToDelete = new int[] { 4, 2 };
        foreach (int pageNum in pagesToDelete)
        {
            if (pageNum >= 1 && pageNum <= pdfDoc.Pages.Count)
            {
                pdfDoc.Pages.Delete(pageNum);
            }
        }

        // 4. Save the modified PDF to another temporary file.
        string tempModifiedPath = Path.GetTempFileName();
        pdfDoc.Save(tempModifiedPath);

        // 5. Apply a 2‑up layout using PdfFileEditor.MakeNUp (the correct API in current Aspose versions).
        //    The method expects an array of source file paths, an output file path, and a flag indicating
        //    whether the resulting pages should be in landscape orientation.
        string tempNupPath = Path.GetTempFileName();
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool nupSuccess = fileEditor.MakeNUp(new string[] { tempModifiedPath }, tempNupPath, false);
        if (!nupSuccess)
            throw new InvalidOperationException("MakeNUp operation failed.");

        // 6. Copy the final PDF from the temporary file into the provided output stream.
        using (var resultFile = new FileStream(tempNupPath, FileMode.Open, FileAccess.Read))
        {
            resultFile.CopyTo(outputPdf);
        }
        // Reset the output stream position for the caller.
        if (outputPdf.CanSeek)
            outputPdf.Position = 0;

        // 7. Clean up temporary files.
        try { File.Delete(tempInputPath); } catch { }
        try { File.Delete(tempModifiedPath); } catch { }
        try { File.Delete(tempNupPath); } catch { }
    }
}
